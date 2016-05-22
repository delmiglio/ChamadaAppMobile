using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile.VO
{
    public class ChamadaForPresencaVO
    {      
        public int Id { get; set; }

        public int AlunoId { get; set; }

        public string Situacao { get; set; }

        public string Materia { get; set; }

        public string Professor { get; set; }
    }
}
