using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CatFood
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int i = 0; //TESTDAY
        DateTime editing;
        DateTime endFoodDateTime;
        TimeSpan timeSpan;
        List<ClockFood> read;
        int[] masFontSizes;


        public MainPage(int[] _masFontSizes)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            masFontSizes = _masFontSizes;

            #region FontSizePage (пока что не знаю как передать в xaml объект)
            labelDate.FontSize = masFontSizes[1];
            labelClock.FontSize = masFontSizes[0];
            labelClockPassed.FontSize = masFontSizes[1];
            buttonClick.FontSize = masFontSizes[4];
            //newDayTest.FontSize = masFontSizes[4];
            buttonFeed.FontSize = masFontSizes[2];
            minusHour.FontSize = masFontSizes[4];
            plusHour.FontSize = masFontSizes[4];
            deleteLast.FontSize = masFontSizes[5];
            labelVisible1.FontSize = masFontSizes[3];
            labelVisible2.FontSize = masFontSizes[3];
            labelVisible3.FontSize = masFontSizes[3];
            labelVisible4.FontSize = masFontSizes[3];
            labelVisible5.FontSize = masFontSizes[3];
            labelVisible6.FontSize = masFontSizes[3];
            labelFood1.FontSize = masFontSizes[3];
            labelFood2.FontSize = masFontSizes[3];
            labelFood3.FontSize = masFontSizes[3];
            labelFood4.FontSize = masFontSizes[3];
            labelFood5.FontSize = masFontSizes[3];
            labelFood6.FontSize = masFontSizes[4];
            #endregion

            SaveEndTime();

            endFoodDateTime = DateTime.Now;

            StackInfo();
            Device.StartTimer(new TimeSpan(0, 0, 1), RefreshAll);
        }

        private bool RefreshAll()
        {
            RefreshTime();
            RefreshDisplayBattery();

            return true;
        }

        private void RefreshDisplayBattery()
        {
            var state = Battery.State;
            if (Battery.ChargeLevel >= 0.9)
            {
                if (BatteryState.Discharging == state) // Отключить зарядку
                {
                    gridMaxBattery.IsVisible = false;
                    labelMaxBattery.IsVisible = false;
                }
                else
                {
                    gridMaxBattery.IsVisible = true;
                    labelMaxBattery.IsVisible = true;
                }
            }
            else if (Battery.ChargeLevel <= 0.35)
            {
                if (BatteryState.Charging == state) // Включить зарядку
                {
                    gridMinBattery.IsVisible = false;
                    labelMinBattery.IsVisible = false;
                }
                else
                {
                    gridMinBattery.IsVisible = true;
                    labelMinBattery.IsVisible = true;
                }
            }
        }

        #region labelVisible
        private void LabelVisible()
        {
            labelVisible1.IsVisible = false;
            labelVisible2.IsVisible = false;
            labelVisible3.IsVisible = false;
            labelVisible4.IsVisible = false;
            labelVisible5.IsVisible = false;
            labelVisible6.IsVisible = false;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            LabelVisible();
            labelVisible1.IsVisible = true;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            LabelVisible();
            labelVisible2.IsVisible = true;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            LabelVisible();
            labelVisible3.IsVisible = true;
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            LabelVisible();
            labelVisible4.IsVisible = true;
        }
        private void Button_Clicked_5(object sender, EventArgs e)
        {
            LabelVisible();
            labelVisible5.IsVisible = true;
        }
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            LabelVisible();
            labelVisible6.IsVisible = true;
        }
        #endregion

        private async void ButtonClick_Clicked(object sender, EventArgs e)
        {
            read = await App.Database.GetItemsAsync();
            await Navigation.PushAsync(new Days(read, masFontSizes));
        }

        private async void ButtonFeed_Clicked(object sender, EventArgs e)
        {
            ClockFood food = new ClockFood()
            {
                DayAndMonth = DateTime.Now.AddDays(i) - new TimeSpan(editing.Hour, editing.Minute, editing.Minute),
            };

            if (labelVisible1.IsVisible)
                food.Food = labelFood1.Text;
            else if (labelVisible2.IsVisible)
                food.Food = labelFood2.Text;
            else if (labelVisible3.IsVisible)
                food.Food = labelFood3.Text;
            else if (labelVisible4.IsVisible)
                food.Food = labelFood4.Text;
            else if (labelVisible5.IsVisible)
                food.Food = labelFood5.Text;
            else if (labelVisible6.IsVisible)
                food.Food = labelFood6.Text;

            await App.Database.SaveItemAsync(food);

            RefreshSetEndTime();

            editing = DateTime.MinValue;

            RefreshTime();
            StackInfo();
        }

        private void ButtonMinusHour_Clicked(object sender, EventArgs e)
        {
            editing = editing.AddHours(1);
            RefreshTime();
        }

        private void ButtonPlusHour_Clicked(object sender, EventArgs e)
        {
            try
            {
                editing = editing.AddHours(-1);
            }
            catch (Exception)
            {
            }
            RefreshTime();
        }
        private async void SaveEndTime()
        {
            await App.Database.SaveItemAsync(new ClockFood { Food = "StartClockFood", DayAndMonth = DateTime.Now });
            RefreshSetEndTime();
        }
        private async void RefreshSetEndTime()
        {
            read = await App.Database.GetItemsAsync();
            endFoodDateTime = read.Last().DayAndMonth;
        }

        private async void ButtonRemove(object sender, EventArgs e)
        {
            read = await App.Database.GetItemsAsync();
            if (read.Count > 1)
            {
                await App.Database.DeleteItemAsync(read.Last());
            }

            read = await App.Database.GetItemsAsync();
            endFoodDateTime = read.Last().DayAndMonth;

            RefreshTime();
            StackInfo();
        }

        private void RefreshTime()
        {
            timeSpan = DateTime.Now - editing;
            labelClockPassed.Text = TimeSpanToString(DateTime.Now - endFoodDateTime);
            labelDate.Text = $"{DateTime.Now.AddDays(i).ToString(@"dd/MM/yyyy")}";
            labelClock.Text = $"{TimeSpanToString(timeSpan)}";
        }
        private async void StackInfo()
        {
            read = await App.Database.GetItemsAsync();
            stackClockDay.Children.Clear();
            if (read.Count < 10)
            {
                for (int i = read.Count - 1; i >= 0; i--)
                {
                    StackAddClock(i);
                }
            }
            else
            {
                for (int i = read.Count - 1; i >= read.Count - 10; i--)
                {
                    StackAddClock(i);
                }
            }
        }

        private void StackAddClock(int index)
        {
            Label label = new LabelCustom //можно было через listView было бы лучше (я о нем позже узнал)
            {
                FontSize = masFontSizes[3],
                Text = $"{DateDateTimeToString(read[index].DayAndMonth)} - {read[index].Food}",
            };
            stackClockDay.Children.Add(label);
        }

        private string DateDateTimeToString(DateTime dateTime)
        {
            return $"{dateTime.ToString(@"HH:mm:ss")}";
        }
        private string TimeSpanToString(TimeSpan timeSpan)
        {
            return $"{timeSpan.ToString(@"hh\:mm\:ss")}";
        }

        private void TestDay(object sender, EventArgs e) //TESTDAY
        {
            i++;
        }
    }
}
