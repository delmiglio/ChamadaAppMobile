using ChamadaAppMobile.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChamadaAppMobile
{
    public class ContentPageLogin : ContentPage
    {
        public ContentPageLogin()
        {
            Label labelLogin = new Label
            {
                Text = "AUTENTICAÇÃO",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White
            };

            Entry txtLogin = new Entry
            {
                Placeholder = "Login",
                VerticalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Numeric,
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
                Text = "Autenticar",
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                FontAttributes = FontAttributes.Bold,
                BorderWidth = 2,
                BorderRadius = 2,
                BorderColor = Color.White,
                BackgroundColor = Color.FromHex("4F95BE"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
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

            btnLogar.Clicked += async (sender, args) =>
            {
                if (string.IsNullOrWhiteSpace(txtLogin.Text))
                {
                    await DisplayAlert("Login Inválido", "Informe corretamente o Login", "OK");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    await DisplayAlert("Senha Inválida", "Informe corretamente a Senha", "OK");
                    return;
                }
                else
                {
                    await Task.Delay(500);
                    await Navigation.PushAsync(new ContentPageHome());
                }
            };

            this.BackgroundColor = Color.FromHex("1B4B67");
            this.Padding = new Thickness(10);
        }

        private bool _canClose = true;

        protected override bool OnBackButtonPressed()
        {
            if (_canClose)
            {
                ShowExitDialog();
            }

            return _canClose;
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Sair", "Deseja fechar o aplicativo?", "Sim", "Não");

            if (answer)
            {
                _canClose = false;

                if(!OnBackButtonPressed())
                {
                    throw new Exception();
                }
            }
        }
    }
}
