using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile
{
    public class ViewModel
    {
        public ObservableCollection<Item> Items { get; set; }

        public ViewModel()
        {
            this.Items = new ObservableCollection<Item>();

            this.Items.Add(new Item
            {
                Nome = "Daniel",
                Sobrenome = "Noronha Canuto",
                Selected = true
            });

            this.Items.Add(new Item
            {
                Nome = "Daniel",
                Sobrenome = "Noronha Canuto",
                Selected = false
            });

            this.Items.Add(new Item
            {
                Nome = "Daniel",
                Sobrenome = "Noronha Canuto",
                Selected = true
            });

            this.Items.Add(new Item
            {
                Nome = "Daniel",
                Sobrenome = "Noronha Canuto",
                Selected = false
            });

            this.Items.Add(new Item
            {
                Nome = "Daniel",
                Sobrenome = "Noronha Canuto",
                Selected = true
            });
        }

        public class Item : INotifyPropertyChanged
        {     
            public event PropertyChangedEventHandler PropertyChanged;
                        
            string nome;
            public string Nome
            {
                get
                {
                    return nome;
                }
                set
                {
                    nome = value;
                    this.Notify("Nome");
                }
            }

            string sobrenome;
            public string Sobrenome
            {
                get
                {
                    return sobrenome;
                }
                set
                {
                    sobrenome = value;
                    this.Notify("Sobrenome");
                }
            }

            bool selected;
            public bool Selected
            {
                get
                {
                    return selected;
                }
                set
                {
                    selected = value;
                    this.Notify("Selected");
                }
            }

            private void Notify(string propertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
