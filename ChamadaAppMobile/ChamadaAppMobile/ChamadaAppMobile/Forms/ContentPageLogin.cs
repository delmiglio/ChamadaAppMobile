﻿using ChamadaApp.Api.Utils;
using ChamadaAppMobile.VO;
using ChamadaAppMobile.Forms;
using ChamadaAppMobile.Services;
using ChamadaAppMobile.Utils;
using System.Diagnostics;
using Xamarin.Forms;

namespace ChamadaAppMobile
{
    public class ContentPageLogin : ContentPageBase
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
                Placeholder = "Nº MATRÍCULA",
                VerticalOptions = LayoutOptions.Center,
                Keyboard = Keyboard.Numeric,
                Opacity = 50
            };

            Entry txtSenha = new Entry
            {
                Placeholder = "SENHA",
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
                    Autenticar(txtLogin.Text, txtSenha.Text);
                }
            };

            this.BackgroundColor = Color.FromHex("1B4B67");
            this.Padding = new Thickness(10);
        }

        private void Autenticar(string login, string senha)
        {
            ConsumeRest autenticar = new ConsumeRest();

            UsuarioVO usuario = new UsuarioVO();
            usuario.Login = login;
            usuario.Senha = senha;

            autenticar.PostResponse<Retorno>("login/Autenticar", usuario).ContinueWith(t =>
            {
                //O ContinueWith é responsavel por fazer algo após o request finalizar
                //Aqui verificamos se houve problema ne requisição
                if (t.IsFaulted)
                {
                    Debug.WriteLine(t.Exception.Message);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Falha", "Ocorreu um erro na Requisição.", "Ok");
                    });
                }
                //Aqui verificamos se a requisição foi cancelada por algum Motivo
                else if (t.IsCanceled)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Cancela", "Requisição Cancelada.", "Ok");
                    });
                }
                //Caso a requisição ocorra sem problemas, cairemos aqui
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Retorno obj;
                        UsuarioVO user = new UsuarioVO();

                        if (t.Result is Retorno)
                        {
                            obj = (Retorno)t.Result;

                            if ((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.Sucesso && obj.ObjRetorno != null)
                            {
                                if (obj.ObjTypeName == user.GetType().Name)
                                {
                                    user = Metodos.JsonToCustomObject<UsuarioVO>(obj.ObjRetorno);
                                }

                                if (App.DataBase.GetUsuario(user.Id) == null)
                                    App.DataBase.SaveUsuario(user);

                                Autenticar();
                            }
                            else if ((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.SemRetorno)
                            {
                                DisplayAlert(obj.RetornoMensagem, obj.RetornoDescricao, "Ok");
                            }
                            else
                            {
                                DisplayAlert(obj.RetornoMensagem, obj.RetornoDescricao, "Ok");
                            }
                        }
                    });
                }
            });
        }

        private void Autenticar()
        {
            ConsumeRest autenticar = new ConsumeRest();

            autenticar.PostResponse<Retorno>("login/Autenticar", App.DataBase.GetUniqueUser()).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Debug.WriteLine(t.Exception.Message);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Falha", "Ocorreu um erro na Requisição.", "Ok");
                    });
                }
                else if (t.IsCanceled)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Falha", "Requisição Cancelada.", "Ok");
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Sucesso)
                        {                            
                            App.GetHome();
                        }    
                    });
                }
            });
        }
    }
}
