using ChamadaAppMobile.Utils.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class ContentPageHomeProfessor : ContentPageBase
    {
        public ContentPageHomeProfessor()
        {
            Label lbUsuario = new Label
            {
                Text = "Olá, " + usuario.ToString() + ".",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.FromHex("1B4B67"),
                FontAttributes = FontAttributes.Bold
            };

            Button btnAbriChamada = new Button
            {
                Text = "Abrir Nova Chamada",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("1B4B67"),
                BorderWidth = 5,
                BorderColor = Color.FromHex("1B4B67"),
                Margin = new Thickness(0, 15),
            };

            btnAbriChamada.Clicked += (sender, args) =>
            {
                App.Current.MainPage = new PageAbrirChamada(usuario);
            };

            Button btnVerificarChamada = new Button
            {
                Text = "Verificar Chamada em Andamento",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("1B4B67"),
                BorderWidth = 5,
                BorderColor = Color.FromHex("1B4B67"),
                Margin = new Thickness(0, 15),
            };

            btnVerificarChamada.Clicked += (sender, args) =>
            {
                App.Current.MainPage = new PageVerificarChamada(usuario);
            };

            StackLayout conteudo = new StackLayout
            {
                BackgroundColor = Color.White,

                Children =
                {
                    lbUsuario,
                    btnAbriChamada,
                    btnVerificarChamada
                },

                Padding = new Thickness(20)
            };


            this.Content = new StackLayout
            {
                Children =
                {
                    GetHeader("Página Inicial"),
                    conteudo,
                    GetFooter()
                }
            };

            this.BackgroundColor = Color.White;           
        }
    }
}
