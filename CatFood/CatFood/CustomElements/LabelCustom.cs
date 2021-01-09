using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CatFood
{
    class LabelCustom : Label
    {
        public LabelCustom()
        {
            TextColor = Color.Black;
            FontSize = 50;
            FontAttributes = FontAttributes.Italic;
        }
    }
}
