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
    public partial class DayInfo : ContentPage
    {
        int[] masFontSizes;
        public DayInfo(List<ClockFood> read, string dayId , int [] _masFontSizes)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            masFontSizes = _masFontSizes;

            StackLayout stack = new StackLayout();
            ScrollView scroll = new ScrollView() { Content = stack, };
            Label labelStart = new Label()
            {
                TextColor = Color.Black,
                Text = "Часы : Минуты : Секунды - Еда",
                FontSize = masFontSizes[0],
            };

            StackInfo(stack, dayId, labelStart, read);
            //InitializeComponent();

            Content = scroll;
        }
        private void StackInfo(StackLayout stack, string dayId, Label labelStart, List<ClockFood> read)
        {
            stack.Children.Clear();
            stack.Children.Add(labelStart);
            for (int i = read.Count - 1; i >= 0; i--)
            {
                if (DateTimeToString(read[i].DayAndMonth) == dayId)
                {
                    Label label = new LabelCustom()
                    {
                        FontSize = masFontSizes[2],
                        Text = $"{read[i].DayAndMonth.ToString(@"HH:mm:ss")} - {read[i].Food}",
                    };
                    stack.Children.Add(label);
                }
            }
        }
        private string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString(@"dd\.MM");
        }
    }
}