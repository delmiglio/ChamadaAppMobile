using ChamadaApp.Domain.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class ContentPageHome : ContentPage
    {
        UsuarioVO usuario;

        public ContentPageHome()
        {
            InicializarUsuario();

            ContentView Titulo = new ContentView
            {
                BackgroundColor = Color.FromHex("1B4B67"),
                Padding = new Thickness(25),
                HorizontalOptions = LayoutOptions.Fill,
                Content = new Label
                {
                    Text = "Página Inicial",
                    FontSize = 30,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                }
            };

            ContentView footer = new ContentView
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

            Label lbUsuario = new Label
            {
                Text = "Olá, " + usuario.ToString() + ".",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.FromHex("1B4B67"),
                FontAttributes = FontAttributes.Bold
            };

            StackLayout conteudo = new StackLayout
            {
                BackgroundColor = Color.White,

                Children =
                {
                    lbUsuario
                },

                Padding = new Thickness(20)
            };

            this.Content = new StackLayout
            {
                Children =
                {
                    Titulo,
                    conteudo,
                    footer
                }
            };

            this.BackgroundColor = Color.White;
        }

        private void InicializarUsuario()
        {
            usuario = App.DataBase.GetUniqueUser();
        }
    }
}
