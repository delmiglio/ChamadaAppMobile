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
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                }
            };
        }

        public Button GetButtonDefault(string titulo)
        {
            return new Button
            {
                Text = titulo,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,//Color.FromHex("1B4B67"),
                BorderWidth = 3,
                BorderRadius = 10,
                BackgroundColor = Color.FromHex("AFC8D6"),
                BorderColor = Color.FromHex("1B4B67"),
                Margin = new Thickness(0, 15)
            };
        }

        public ContentView GetFooter()
        {
            return new ContentView
            {
                BackgroundColor = Color.FromHex("1B4B67"),
                Padding = new Thickness(15),
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Fill,

                Content = new Label
                {
                    Text = "Trabalho de Conclusão de Curso",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                }
            };
        }

        public Label GetLabelDefaul()
        {
            return new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White
            };
        }

        public ContentView GetMessageDefault()
        {
            return new ContentView
            {
                BackgroundColor = Color.FromHex("434E4E"),
                Padding = new Thickness(20, 15),
                Margin = new Thickness(0, 10),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                IsVisible = true,
                Content = new Label
                {
                    Text = "AGUARDE...",
                    FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                }
            };
        }
    }
}
