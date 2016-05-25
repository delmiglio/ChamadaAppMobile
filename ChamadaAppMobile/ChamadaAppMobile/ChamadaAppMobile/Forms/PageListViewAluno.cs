using ChamadaAppMobile.Utils.VO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ChamadaAppMobile.Forms
{
    public class PageListViewAluno : MasterPage
    {
        Button btnConcluirChamada;
        ChamadaVO chamada;
        ListView listAlunos;

        public PageListViewAluno()
        {
            Label header = new Label
            {
                Text = "Alunos aguardando presença",
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.FromHex("1B4B67"),
                FontAttributes = FontAttributes.Bold
            };

            listAlunos = ConfiguraLit(Teste());

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

            StackLayout conteudo = new StackLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Children =
                {
                    header,
                    decricao
                }
            };

            if (listAlunos == null)
                conteudo.Children.Add(GetMessageDefault());
            else
                conteudo.Children.Add(listAlunos);

            btnConcluirChamada = new Button
            {
                Text = "CONCLUIR CHAMADA",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("1B4B67"),
                BorderWidth = 5,
                BorderColor = Color.FromHex("1B4B67"),
                Margin = new Thickness(0, 10),
            };

            btnConcluirChamada.Clicked += async (sender, args) =>
            {
                bool resposta = await DisplayAlert("ATENÇÂO", "Ao concluir a chamada a mesma não estará mais disponível para alterações. Deseja Concluir?", "Concluir", "Cancelar");
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
        
        private ObservableCollection<AlunoChamadaVO> Teste()
        {
            ObservableCollection<AlunoChamadaVO> alunos = new ObservableCollection<AlunoChamadaVO>();

            for (int i = 0; i < 5; i++)
            {
                bool ativo = false;
                if (i % 2 == 0)
                    ativo = true;

                alunos.Add(new AlunoChamadaVO
                {
                    Id = i + 1,
                    alunoNome = "Aluno Teste " + (i + 1).ToString(),
                    sitAlunoChamada = "Aguardando Presença",
                    Selected = ativo
                });
            }

            return alunos;
        }
    }
}
