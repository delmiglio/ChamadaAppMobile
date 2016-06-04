using ChamadaApp.Api.Utils;
using ChamadaAppMobile.Services;
using ChamadaAppMobile.Utils;
using ChamadaAppMobile.Utils.Enum;
using ChamadaAppMobile.Utils.VO;
using ChamadaAppMobile.VO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class PageListViewAluno : MasterPage
    {
        Button btnConcluirChamada;
        ChamadaVO chamada;
        ListView listAlunos;
        StackLayout conteudo;
        ContentView info;

        public PageListViewAluno(ChamadaVO chamadaAberta, List<AlunoChamadaVO> alunos)
        {
            this.chamada = chamadaAberta;

            Label header = new Label
            {
                Text = "Alunos aguardando presença",
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.FromHex("1B4B67"),
                FontAttributes = FontAttributes.Bold
            };

            StackLayout decricao = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, Device.OnPlatform(20, 5, 5), 10, 5),

                Children =
                {
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        Text = "Aluno",
                        TextColor = Color.FromHex("1B4B67"),
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold
                    },
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Text = "Marcar Presença",
                        TextColor = Color.FromHex("1B4B67"),
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold
                    }
                }
            };

            conteudo = new StackLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Children =
                {
                    header,
                    decricao
                }
            };

            info = GetMessageDefault();

            if (alunos == null || alunos.Count == 0)
            {
                info.BackgroundColor = Color.FromHex("328325");

                info.Content = new Label
                {
                    Text = "Todos os alunos estão presentes. Conclua a chamada!",
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.White
                };
            }

            ObservableCollection<AlunoChamadaVO> alunosNaoPresentes = Metodos.ListToObservableCollection(alunos);
            listAlunos = ConfiguraLit(alunosNaoPresentes);

            if (alunos == null || alunos.Count == 0)
                conteudo.Children.Add(info);
            else
                conteudo.Children.Add(listAlunos);

            btnConcluirChamada = GetButtonDefault("CONCLUIR CHAMADA");
            btnConcluirChamada.Margin = new Thickness(0, 10);

            btnConcluirChamada.Clicked += async (sender, args) =>
            {
                bool resposta = await DisplayAlert("ATENÇÂO", "Ao concluir a chamada a mesma não estará mais disponível para alterações. Deseja Concluir?", "Concluir", "Cancelar");

                if (resposta)
                    ConcluirChamada((listAlunos.ItemsSource as ObservableCollection<AlunoChamadaVO>));
            };

            this.BackgroundColor = Color.White;

            this.Content = new StackLayout
            {
                Children =
                {
                    GetHeader("Concluir Chamada"),
                    conteudo,
                    btnConcluirChamada,
                    GetFooter()
                }
            };
        }

        private void ConcluirChamada(ObservableCollection<AlunoChamadaVO> sourceList)
        {
            List<AlunoChamadaAlteracaoVO> alunosPresentes = new List<AlunoChamadaAlteracaoVO>();

            foreach (AlunoChamadaVO aluno in sourceList)
            {
                if (aluno.Selected)
                {
                    alunosPresentes.Add(new AlunoChamadaAlteracaoVO
                    {
                        Id = aluno.Id,
                        chamadaId = chamada.Id,
                        sitAlunoChamadaId = (int)SitAlunoChamadaEnum.PresencaConfirmada
                    });
                }
            }

            Retorno param = new Retorno();
            param.ObjRetorno = chamada;
            param.ListRetorno = alunosPresentes.Cast<object>().ToList();

            ConsumeRest concluirChamada = new ConsumeRest();

            concluirChamada.PostResponse<Retorno>("chamada/ConcluirChamada", param).ContinueWith(t =>
            {
                if (t.IsCompleted && t.Result != null)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Sucesso)
                        {
                            conteudo = new StackLayout
                            {
                                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                                Children =
                                {
                                    info
                                }
                            };

                            Label lb = GetLabelDefaul();

                            lb.Text = t.Result.RetornoMensagem + ((!string.IsNullOrWhiteSpace(t.Result.RetornoDescricao)) ?
                                                                (Environment.NewLine + t.Result.RetornoDescricao) : "");

                            info.BackgroundColor = Color.FromHex("328325");
                            info.IsVisible = true;
                            info.Content = lb;

                            await Task.Delay(500);
                            App.GetHome();
                        }
                        else if ((TpRetornoEnum)t.Result.TpRetorno == TpRetornoEnum.Erro)
                        {
                            await DisplayAlert(t.Result.RetornoMensagem, t.Result.RetornoDescricao, "OK");
                        }
                    });
                }
            });
        }

        private ObservableCollection<AlunoChamadaVO> GetTeste()
        {
            ObservableCollection<AlunoChamadaVO> lista = new ObservableCollection<AlunoChamadaVO>();

            for(int i = 0; i < 5; i++)
            {
                lista.Add(new AlunoChamadaVO
                {
                    Id = (i + 1),
                    alunoNome = "Aluno Hipotético",
                    sitAlunoChamada = "Aguardando Chamada"
                });
            }

            return lista;
        }

        private ListView ConfiguraLit(ObservableCollection<AlunoChamadaVO> sourceList)
        {
            ListView listView = new ListView
            {
                ItemsSource = sourceList,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label titulo = new Label();
                    titulo.SetBinding(Label.TextProperty, "alunoNome");
                    titulo.FontSize = 25;
                    titulo.TextColor = Color.FromHex("1B4B67");

                    Label descricao = new Label();
                    descricao.SetBinding(Label.TextProperty, "sitAlunoChamada");
                    descricao.FontSize = 20;
                    descricao.TextColor = Color.Red;
                    descricao.FontAttributes = FontAttributes.Bold;

                    Switch switcher = new Switch
                    {
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        VerticalOptions = LayoutOptions.Center
                    };

                    switcher.SetBinding(Switch.IsToggledProperty, "Selected");                    
                    
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.FillAndExpand,

                            Children =
                            {
                                 new StackLayout
                                 {
                                     VerticalOptions = LayoutOptions.Center,
                                     Spacing = 0,
                                     Children =
                                     {
                                         titulo,
                                         descricao
                                     }
                                 },
                                 switcher
                            }
                        }
                    };
                })
            };

            return listView;
        }
    }
}
