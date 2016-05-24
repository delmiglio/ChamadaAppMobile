using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class MasterPage : ContentPage
    {
        public MasterPage()
        {
            
        }

        public ContentView GetHeader(string titulo)
        {
            return new ContentView
            {
                BackgroundColor = Color.FromHex("1B4B67"),
                Padding = new Thickness(20),
                HorizontalOptions = LayoutOptions.Fill,
                Content = new Label
                {
                    Text = titulo,
                    FontSize = 30,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                }
            };
        }

        public ContentView GetFooter()
        {
            return new ContentView
            {
                BackgroundColor = Color.FromHex("1B4B67"),
                Padding = new Thickness(20),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Fill,

                Content = new Label
                {
                    Text = "Trabalho de Conclusão de Curso",
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                }
            };
        }

        public ContentView GetMessageDefault()
        {
            return new ContentView
            {
                BackgroundColor = Color.FromHex("434E4E"),
                Padding = new Thickness(20, 15),
                Margin = new Thickness(0, 20),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                IsVisible = true,
                Content = new Label
                {
                    Text = "AGUARDE...",
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                }
            };
        }
    }
}
