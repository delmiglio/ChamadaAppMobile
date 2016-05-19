using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile.VO
{
    /// <summary>
    /// Essa classe possui as propriedades a serem retornadas em uma requisição...
    /// </summary>
    public class Retorno
    {
        public int TpRetorno { get; set; }

        public string RetornoMensagem { get; set; }

        public string RetornoDescricao { get; set; }

        public object ObjRetorno { get; set; }

        public string ObjTypeName { get; set; }

        public List<object> ListRetorno { get; set; }
    }
}
