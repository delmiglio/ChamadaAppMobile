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

            StackLayout conteudo = new StackLayout
            {
                BackgroundColor = Color.White,

                Children =
                {
                    lbUsuario,
                    dadosChamada
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

            GetMateriaChamada(dadosChamada);
        }

        private void InicializarUsuario()
        {
            usuario = App.DataBase.GetUniqueUser();
        }

        private void GetMateriaChamada(ContentView view)
        {
            Label lb = new Label
            {                
                FontSize = 20,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White
            };

            GetRest getChamada = new GetRest();

            string parametros = string.Format("alunoId={0}", 3);//usuario.Id);

            getChamada.GetResponse<Retorno>("chamada", parametros).ContinueWith(t =>
            {                
                if(t.IsCompleted)
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
                            else if((TpRetornoEnum)obj.TpRetorno == TpRetornoEnum.Erro)
                            {
                                view.BackgroundColor = Color.FromHex("A63030");                                
                            }

                            lb.Text = obj.RetornoMensagem + ((!string.IsNullOrWhiteSpace(obj.RetornoDescricao)) ? 
                                                                (Environment.NewLine + obj.RetornoDescricao) : "");
                            view.IsVisible = true;
                            view.Content = lb;
                        }
                    });
                }
            });
        }
    }
}
