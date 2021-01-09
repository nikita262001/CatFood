using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatFood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FontSizePages : ContentPage
    {
        Label label;
        public FontSizePages()
        {
            //InitializeComponent(); // 70 60 50 40 30 25

            Button buttonUWP = new Button { Text = "UWP" };
            buttonUWP.Clicked += UWPFontSize;
            Button buttonTheTablet = new Button { Text = "Планшет" };
            buttonTheTablet.Clicked += TheTabletFontSize;
            Button buttonPhone = new Button { Text = "Телефон" };
            buttonPhone.Clicked += PhoneFontSize;

            label = new Label {Margin = new Thickness(0,5,10,0)};
            CheckBox check = new CheckBox
            {
                IsChecked = DeviceDisplay.KeepScreenOn,
            };
            check.CheckedChanged += CheckBox_CheckedChanged;
            StackLayout stack = new StackLayout 
            {
                Children = { label, check },
                Orientation = StackOrientation.Horizontal,
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { buttonUWP, buttonTheTablet, buttonPhone, stack },
                WidthRequest = 200
            };
            InfoBlockDisplayOnLabel();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            DeviceDisplay.KeepScreenOn = !DeviceDisplay.KeepScreenOn;
            InfoBlockDisplayOnLabel();
        }

        private void InfoBlockDisplayOnLabel()
        {
            if (DeviceDisplay.KeepScreenOn == true)
            {
                label.Text = "Авто блокировка выключена";
            }
            else
            {
                label.Text = "Авто блокировка включена";
            }
        }

        private void UWPFontSize(object sender, EventArgs e)
        {
            int[] sizes = { 70, 60, 50, 40, 30, 25 };

            Navigation.PushAsync(new MainPage(sizes));
        }
        private void TheTabletFontSize(object sender, EventArgs e)
        {
            int[] sizes = { 60, 50, 40, 30, 20, 15 };
            Navigation.PushAsync(new MainPage(sizes));
        }
        private void PhoneFontSize(object sender, EventArgs e)
        {
            int[] sizes = { 35, 30, 25, 20, 15, 12 };
            Navigation.PushAsync(new MainPage(sizes));
        }
    }
}