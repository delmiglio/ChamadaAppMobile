using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile.Utils.VO
{
    public class MateriaForChamadaVO
    {
        public int HorarioMaterioProfTurmaId { get; set; }

        public int TurmaId { get; set; }

        public string MateriaDescricao { get; set; }

        public string ModuloDescricao { get; set; }

        public string CursoDescricao { get; set; }
    }
}
