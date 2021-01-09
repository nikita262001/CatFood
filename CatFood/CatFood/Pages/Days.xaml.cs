using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Days : ContentPage
    {
        bool newDays = true;
        List<string> days = new List<string>();
        List<ClockFood> read;
        DateTime dateId;
        int[] masFontSizes;
        public Days(List<ClockFood> _read , int [] _masFontSizes)
        {
            masFontSizes = _masFontSizes;
            NavigationPage.SetHasNavigationBar(this, false);

            read = _read;
            days.Add(DateTimeToString(DateTime.Now));

            foreach (ClockFood item in read)
            {
                foreach (string stringDays in days)
                {
                    if (DateTimeToString(item.DayAndMonth) == stringDays)
                    {
                        newDays = false;
                    }
                }
                if (newDays)
                {
                    days.Add(DateTimeToString(item.DayAndMonth));
                }
                newDays = true;
            }

            //InitializeComponent();
            FlexLayout flex = new FlexLayout() { Padding = 50, Wrap = FlexWrap.Wrap, };
            ScrollView scroll = new ScrollView { Content = flex, };

            for (int i = days.Count - 1; i >= 0; i--)
            {
                Button button = new Button
                {
                    FontSize = masFontSizes[2],
                    HeightRequest = 75,
                    WidthRequest = 125,
                    Text = days[i],
                };
                button.Clicked += Btn_Click;
                flex.Children.Add(button);
            }

            Content = scroll;
        }
        private async void Btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            foreach (ClockFood item in await App.Database.GetItemsAsync())
            {
                if (DateTimeToString(item.DayAndMonth) == button.Text)
                {
                    dateId = item.DayAndMonth;
                    await Navigation.PushAsync(new DayInfo(read, button.Text , masFontSizes));
                    break;
                }
            }
        }
        private string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString(@"dd\.MM");
        }
    }
}