using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile
{
    public class ContentPageLogin : ContentPage
    {
        public ContentPageLogin()
        {
            Label labelLogin = new Label
            {
                Text = "LOGIN",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White                               
            };

            Entry txtLogin = new Entry
            {
                Placeholder = "Login",
                VerticalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,
                Opacity = 50
            };

            Entry txtSenha = new Entry
            {
                Placeholder = "Senha",
                VerticalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Text,                
                IsPassword = true,
                Opacity = 50
            };

            Button btnLogar = new Button
            {
                Text = "LOGAR",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BorderWidth = 2,
                BorderRadius = 2,
                WidthRequest = 200,
                BorderColor = Color.FromHex("075F9E"),
                BackgroundColor = Color.FromHex("023457"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Content = new StackLayout
            {
                Children =
                {
                    labelLogin,
                    txtLogin,
                    txtSenha,
                    btnLogar
                }
            };

            btnLogar.Clicked += (sender, args) =>
            {
                if (txtLogin.Text == null)
                {
                    DisplayAlert("Login Inválido", "Informe corretamente o Login", "OK");
                    return;
                }
                else if (txtSenha.Text == null)
                    DisplayAlert("Senha Inválida", "Informe corretamente a Senha", "OK");
            };

            this.BackgroundColor = Color.FromHex("075F9E");
            this.Padding = new Thickness(10);
        }        
    }
}
