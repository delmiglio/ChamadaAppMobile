using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile.Utils.VO
{
    public class ChamadaVO
    {
        public int Id { get; set; }

        public string DtChamada { get; set; }

        public string HoraInicio { get; set; }

        public string HoraTermino { get; set; }

        public string SitChamada { get; set; }

        public string Materia { get; set; }

        public string Modulo { get; set; }

        public string Curso { get; set; }
    }
}
