using ChamadaApp.Api.Utils;
using ChamadaApp.Domain.VO;
using ChamadaAppMobile.Services;
using ChamadaAppMobile.Utils;
using ChamadaAppMobile.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class ContentPageHome : ContentPage
    {
        UsuarioVO usuario;
        ChamadaForPresencaVO chamada;

        public ContentPageHome()
        {           
            InicializarUsuario();

            ContentView Header = new ContentView
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

            ContentView dadosChamada = new ContentView
            {
                BackgroundColor = Color.FromHex("434E4E"),
                Padding = new Thickness(20, 15),
                Margin = new Thickness(0, 20),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                IsVisible = false
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
            

            ScrollView scroll = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,   
                IsVisible = false             
            };

            StackLayout conteudo = new StackLayout
            {
                BackgroundColor = Color.White,                              

                Children =
                {
                    lbUsuario,
                    dadosChamada,
                    scroll                    
                },

                Padding = new Thickness(20)
            };


            this.Content = new StackLayout
            {
                Children =
                {
                    Header,
                    conteudo,
                    footer
                }
            };

            this.BackgroundColor = Color.White;

            GetMateriaChamada(dadosChamada, scroll);            
        }

        private void InicializarUsuario()
        {
            usuario = App.DataBase.GetUniqueUser();
        }

        private void GetMateriaChamada(ContentView view, ScrollView scroll)
        {
            Label lb = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White
            };

            GetRest getChamada = new GetRest();

            string parametros = string.Format("alunoId={0}", usuario.Id);

            getChamada.GetResponse<Retorno>("chamada", parametros).ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Retorno obj;

                        if (t.Result is Retorno)
                        {
                            obj = (Retorno)t.Result;

                            if ((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.Sucesso && obj.ObjRetorno != null)
                            {
                                if (obj.ObjTypeName == typeof(ChamadaForPresencaVO).Name)
                                {
                                    chamada = Metodos.JsonToCustomObject<ChamadaForPresencaVO>(obj.ObjRetorno);
                                }

                                view.BackgroundColor = Color.FromHex("328325");
                            }
                            else if ((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.Erro)
                            {
                                view.BackgroundColor = Color.FromHex("A63030");
                            }

                            lb.Text = obj.RetornoMensagem + ((!string.IsNullOrWhiteSpace(obj.RetornoDescricao)) ?
                                                                (Environment.NewLine + obj.RetornoDescricao) : "");
                            view.IsVisible = true;
                            view.Content = lb;

                            GetDadosChamada(chamada, scroll);
                        }
                    });
                }
            });
        }

        private void GetDadosChamada(ChamadaForPresencaVO chamada, ScrollView scroll)
        {
            scroll.IsVisible = true;

            scroll.Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children =
                {
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
                        Text = chamada.Materia,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("1B4B67")
                    },

                    new Label
                    {
                        Text = "Professor",
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.Black
                    },

                    new Label
                    {
                        Text = chamada.Professor,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("1B4B67")
                    },

                    new Label
                    {
                        Text = "Situação",
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.Black
                    },

                    new Label
                    {
                        Text = chamada.Situacao,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Red
                    }
                }
            };
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

                if (!OnBackButtonPressed())
                {
                    throw new Exception();
                }
            }
        }
    }
}
