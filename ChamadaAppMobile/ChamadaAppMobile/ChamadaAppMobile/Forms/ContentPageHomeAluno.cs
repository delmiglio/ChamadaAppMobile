using ChamadaApp.Api.Utils;
using ChamadaAppMobile.VO;
using ChamadaAppMobile.Services;
using ChamadaAppMobile.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class ContentPageHomeAluno : ContentPageBase
    {       
        ChamadaForPresencaVO chamada;

        ScrollView scroll;
        ContentView dadosChamada;
        Button btnResponderChamada;

        public ContentPageHomeAluno()
        {     
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

            dadosChamada = new ContentView
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


            scroll = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };

            btnResponderChamada = new Button
            {
                Text = "Responder Chamada",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("1B4B67"),
                BorderWidth = 5,
                BorderColor = Color.FromHex("1B4B67"),
                Margin = new Thickness(0, 30),
            };

            btnResponderChamada.Clicked += (sender, args) =>
            {
                ResponderChamada(chamada);
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

            GetMateriaChamada();
        }        

        private void GetMateriaChamada()
        {
            Label lb = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White
            };

            ConsumeRest getChamada = new ConsumeRest();

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

                                dadosChamada.BackgroundColor = Color.FromHex("328325");
                                GetDadosChamada(chamada);
                            }
                            else if ((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.Erro)
                            {
                                dadosChamada.BackgroundColor = Color.FromHex("A63030");
                            }

                            lb.Text = obj.RetornoMensagem + ((!string.IsNullOrWhiteSpace(obj.RetornoDescricao)) ?
                                                                (Environment.NewLine + obj.RetornoDescricao) : "");
                            dadosChamada.IsVisible = true;
                            dadosChamada.Content = lb;
                        }
                    });
                }
            });
        }

        private void GetDadosChamada(ChamadaForPresencaVO chamada)
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
                    },

                    btnResponderChamada                    
                }
            };
        }        

        private void ResponderChamada(ChamadaForPresencaVO alunoChamada)
        {
            Label lb = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White
            };

            ConsumeRest putChamada = new ConsumeRest();            

            putChamada.PutResponse<Retorno>("chamada", alunoChamada).ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Retorno obj;

                        if (t.Result is Retorno)
                        {
                            obj = (Retorno)t.Result;

                            if ((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.Sucesso)
                            {
                                dadosChamada.BackgroundColor = Color.FromHex("328325");
                                scroll.Content = null;
                            }
                            else if ((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.Erro)
                            {
                                dadosChamada.BackgroundColor = Color.FromHex("A63030");
                            }

                            lb.Text = obj.RetornoMensagem + ((!string.IsNullOrWhiteSpace(obj.RetornoDescricao)) ?
                                                                (Environment.NewLine + obj.RetornoDescricao) : "");
                            dadosChamada.IsVisible = true;
                            dadosChamada.Content = lb;
                        }
                    });
                }
            });
        }        
    }
}
