﻿using ChamadaApp.Api.Utils;
using ChamadaAppMobile.Services;
using ChamadaAppMobile.Utils;
using ChamadaAppMobile.Utils.Enum;
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
    public class PageVerificarChamada : MasterPage
    {
        UsuarioVO usuario;
        ChamadaVO chamada;

        ContentView dadosChamada;
        Button btnEncerrarChamada;
        ScrollView bodyContent = new ScrollView();

        public PageVerificarChamada(UsuarioVO user)
        {
            this.usuario = user;

            dadosChamada = GetMessageDefault();

            btnEncerrarChamada = GetButtonDefault("ENCERRAR CHAMADA");
            btnEncerrarChamada.Margin = new Thickness(0, 20, 0, 0);

            btnEncerrarChamada.Clicked += async (sender, args) =>
            {
                bool resposta = await DisplayAlert("ATENÇÃO", "Ao encerrar a chamada não será possível receber novas presenças. Deseja Encerrar?", "Encerrar", "Cancelar");

                if (resposta)
                    EncerrarChamada();
            };

            StackLayout conteudo = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = new Thickness(20, 0),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children =
                {
                    dadosChamada,
                    bodyContent
                }
            };

            this.Content = new StackLayout
            {
                Children =
                {
                    GetHeader("Manter Chamada"),
                    conteudo,
                    GetFooter()
                }
            };

            this.BackgroundColor = Color.White;
            GetChamadaAberta();
        }

        private void GetChamadaAberta()
        {
            ConsumeRest getChamada = new ConsumeRest();
            string parametros = string.Format("professorId={0}&sitChamadaId={1}", usuario.Id, (int)SitChamadaEnum.Aberta);

            getChamada.GetResponse<Retorno>("chamada/ChamadaAberta", parametros).ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Sucesso && t.Result.ObjRetorno != null)
                    {
                        if (t.Result.ObjTypeName == typeof(ChamadaVO).Name)
                        {
                            chamada = Metodos.JsonToCustomObject<ChamadaVO>(t.Result.ObjRetorno);
                            ExibeDadosChamada();
                        }

                        dadosChamada.BackgroundColor = Color.FromHex("328325");
                    }
                    else if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Erro)
                    {
                        dadosChamada.BackgroundColor = Color.FromHex("A63030");
                    }

                    Label lb = GetLabelDefaul();

                    lb.Text = t.Result.RetornoMensagem + ((!string.IsNullOrWhiteSpace(t.Result.RetornoDescricao)) ?
                                                            (Environment.NewLine + t.Result.RetornoDescricao) : "");
                    dadosChamada.IsVisible = true;
                    dadosChamada.Content = lb;
                });

            });
        }

        private void ExibeDadosChamada()
        {
            bodyContent.Content = new StackLayout
            {
                IsVisible = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
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
                        Text = chamada.Curso,
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
                        Text = chamada.Modulo,
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
                        Text = chamada.Materia,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("1B4B67")
                    },

                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,

                        Children =
                        {
                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.StartAndExpand,

                                Children =
                                {
                                    new Label
                                    {
                                        Text = "Data",
                                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                        VerticalOptions = LayoutOptions.Start,
                                        HorizontalOptions = LayoutOptions.Start,
                                        TextColor = Color.Black
                                    },

                                    new Label
                                    {
                                        Text = chamada.DtChamada,
                                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                        VerticalOptions = LayoutOptions.Start,
                                        HorizontalOptions = LayoutOptions.Start,
                                        FontAttributes = FontAttributes.Bold,
                                        TextColor = Color.FromHex("1B4B67")
                                    }
                                }
                            },

                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.CenterAndExpand,
                                //Margin = new Thickness(20, 0),

                                Children =
                                {
                                    new Label
                                    {
                                        Text = "Início",
                                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                        VerticalOptions = LayoutOptions.Start,
                                        HorizontalOptions = LayoutOptions.Start,
                                        TextColor = Color.Black
                                    },

                                    new Label
                                    {
                                        Text = chamada.HoraInicio,
                                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                        VerticalOptions = LayoutOptions.Start,
                                        HorizontalOptions = LayoutOptions.Start,
                                        FontAttributes = FontAttributes.Bold,
                                        TextColor = Color.FromHex("1B4B67")
                                    }
                                }
                            },

                            new StackLayout
                            {
                                HorizontalOptions = LayoutOptions.EndAndExpand,

                                Children =
                                {
                                    new Label
                                    {
                                        Text = "Término",
                                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                        VerticalOptions = LayoutOptions.Start,
                                        HorizontalOptions = LayoutOptions.Start,
                                        TextColor = Color.Black
                                    },

                                    new Label
                                    {
                                        Text = string.IsNullOrEmpty(chamada.HoraTermino) ? "--:--:--" : chamada.HoraTermino,
                                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                        VerticalOptions = LayoutOptions.Start,
                                        HorizontalOptions = LayoutOptions.Start,
                                        FontAttributes = FontAttributes.Bold,
                                        TextColor = Color.FromHex("1B4B67")
                                    }
                                }
                            }
                        }
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
                        Text = chamada.SitChamada,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Red
                    },

                    btnEncerrarChamada
                }
            };
        }

        private void EncerrarChamada()
        {
            ConsumeRest encerrarChamada = new ConsumeRest();

            encerrarChamada.PutResponse<Retorno>("chamada/EncerrarChamada", chamada).ContinueWith(t =>
            {
                if (t.IsCompleted)
                {
                    List<AlunoChamadaVO> alunos = null;

                    Device.BeginInvokeOnMainThread(() =>
                    {

                        if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Sucesso)
                        {
                            dadosChamada.BackgroundColor = Color.FromHex("328325");

                            if(t.Result.ListRetorno != null)
                                alunos = Metodos.JsonToCustomObject<AlunoChamadaVO>(t.Result.ListRetorno);

                            App.Current.MainPage = new PageListViewAluno(chamada, alunos);
                        }
                        else if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Erro)
                        {
                            dadosChamada.BackgroundColor = Color.FromHex("A63030");
                        }

                        Label lb = GetLabelDefaul();

                        lb.Text = t.Result.RetornoMensagem + ((!string.IsNullOrWhiteSpace(t.Result.RetornoDescricao)) ?
                                                                (Environment.NewLine + t.Result.RetornoDescricao) : "");
                        dadosChamada.IsVisible = true;
                        dadosChamada.Content = lb;
                    });
                }
            });
        }

        protected override bool OnBackButtonPressed()
        {
            App.GetHome();
            return true;
        }
    }
}
