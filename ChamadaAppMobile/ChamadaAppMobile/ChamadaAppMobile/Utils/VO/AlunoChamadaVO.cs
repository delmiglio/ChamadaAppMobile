using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile.Utils.VO
{
    public class AlunoChamadaVO : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int ChamadaId { get; set; }

        public string alunoNome { get; set; }

        public string sitAlunoChamada { get; set; }

        public string DtPresenca { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            if (Selected)
                sitAlunoChamada = "Presença Confirmada";
            else
                sitAlunoChamada = "Aguardando Presença";
        }
    }
}
