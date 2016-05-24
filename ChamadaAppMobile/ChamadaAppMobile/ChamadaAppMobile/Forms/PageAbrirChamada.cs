using ChamadaApp.Api.Utils;
using ChamadaAppMobile.Services;
using ChamadaAppMobile.Utils;
using ChamadaAppMobile.Utils.VO;
using ChamadaAppMobile.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class PageAbrirChamada : MasterPage
    {
        UsuarioVO usuario;
        MateriaForChamadaVO materia;

        ContentView dadosMateria;
        Button btnAbrirChamada;
        ScrollView scroll = new ScrollView();

        public PageAbrirChamada(UsuarioVO user)
        {
            this.usuario = user;

            dadosMateria = GetMessageDefault();

            btnAbrirChamada = new Button
            {
                Text = "ABRIR CHAMADA",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("1B4B67"),
                BorderWidth = 5,
                BorderColor = Color.FromHex("1B4B67"),
                Margin = new Thickness(0, 30),
            };

            btnAbrirChamada.Clicked += (sender, args) =>
            {
                AbrirNovaChamada();
            };

            StackLayout conteudo = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = new Thickness(20),

                Children =
                {
                    dadosMateria,
                    scroll
                }
            };


            this.Content = new StackLayout
            {
                Children =
                {
                    GetHeader("Abrir Chamada"),
                    conteudo,
                    GetFooter()
                }
            };

            this.BackgroundColor = Color.White;

            GetMateriaNovaChamada();
        }

        private void GetMateriaNovaChamada()
        {
            ConsumeRest getMateria = new ConsumeRest();

            getMateria.GetResponse<Retorno>("chamada", string.Format("professorId={0}", usuario.Id)).ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Sucesso && t.Result.ObjRetorno != null)
                        {
                            if (t.Result.ObjTypeName == typeof(MateriaForChamadaVO).Name)
                            {
                                materia = Metodos.JsonToCustomObject<MateriaForChamadaVO>(t.Result.ObjRetorno);
                                ExibeDadosMateria();
                            }

                            dadosMateria.BackgroundColor = Color.FromHex("328325");
                        }
                        else if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Erro)
                        {
                            dadosMateria.BackgroundColor = Color.FromHex("A63030");
                        }

                        Label lb = new Label
                        {
                            FontSize = 20,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.White
                        };

                        lb.Text = t.Result.RetornoMensagem + ((!string.IsNullOrWhiteSpace(t.Result.RetornoDescricao)) ?
                                                                (Environment.NewLine + t.Result.RetornoDescricao) : "");
                        dadosMateria.IsVisible = true;
                        dadosMateria.Content = lb;
                    });
                }
            });
        }

        private void AbrirNovaChamada()
        {     
            ConsumeRest postNovaChamada = new ConsumeRest();

            postNovaChamada.PostResponse<Retorno>("chamada", materia).ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Sucesso)
                        {
                            dadosMateria.BackgroundColor = Color.FromHex("328325");
                            scroll.Content = null;
                        }
                        else if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Erro)
                        {
                            dadosMateria.BackgroundColor = Color.FromHex("A63030");
                        }

                        Label lb = new Label
                        {
                            FontSize = 20,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.White
                        };

                        lb.Text = t.Result.RetornoMensagem + ((!string.IsNullOrWhiteSpace(t.Result.RetornoDescricao)) ?
                                                            (Environment.NewLine + t.Result.RetornoDescricao) : "");
                        dadosMateria.IsVisible = true;
                        dadosMateria.Content = lb;
                    });
                }
            });
        }

        private void ExibeDadosMateria()
        {
            scroll.IsVisible = true;

            scroll.Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children =
                {
                    new Label
                    {
                        Text = "Curso",
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.Black
                    },

                    new Label
                    {
                        Text = materia.CursoDescricao,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("1B4B67")
                    },

                    new Label
                    {
                        Text = "Módulo",
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.Black
                    },

                    new Label
                    {
                        Text = materia.ModuloDescricao,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("1B4B67")
                    },

                     new Label
                    {
                        Text = "Matéria",
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.Black
                    },

                    new Label
                    {
                        Text = materia.MateriaDescricao,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("1B4B67")
                    },

                    btnAbrirChamada
                }
            };
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = App.GetHome();
            return true;
        }
    }
}
