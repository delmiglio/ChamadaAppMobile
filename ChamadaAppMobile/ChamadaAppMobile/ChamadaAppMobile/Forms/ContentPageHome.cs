﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class ContentPageHome : ContentPage
    {
        public ContentPageHome()
        {
            Label labelHome = new Label
            {
                Text = "HOME",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White
            };                       

            Content = new StackLayout
            {
                Children =
                {
                    labelHome
                }
            };
            

            this.BackgroundColor = Color.FromHex("1B4B67");
            this.Padding = new Thickness(10);
        }
    }
}
