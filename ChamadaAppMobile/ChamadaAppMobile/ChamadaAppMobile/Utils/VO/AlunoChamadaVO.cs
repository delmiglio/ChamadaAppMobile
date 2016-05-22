using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile.Utils.VO
{
    public class AlunoChamadaVO
    {
        public int Id { get; set; }

        public int ChamadaId { get; set; }

        public string alunoNome { get; set; }

        public string sitAlunoChamada { get; set; }

        public string DtPresenca { get; set; }
    }
}
