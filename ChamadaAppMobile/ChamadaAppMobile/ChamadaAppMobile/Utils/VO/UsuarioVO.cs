using Newtonsoft.Json.Linq;
using System;

namespace ChamadaApp.Domain.VO
{
    public class UsuarioVO
    {
        public UsuarioVO()
        {

        }
        /*
        public UsuarioVO(object obj)
        {
            JObject jObject = JObject.Parse(obj);
            JToken jUser = jObject["UsuarioVO"];
            Id = (int)jUser["Id"];
            Nome = (string)jUser["Nome"];
            Sobrenome = (string)jUser["Sobrenome"];
            Login = (string)jUser["Login"];
            Senha = (string)jUser["Senha"];
            Token = (string)jUser["Token"]; ;
            TpUsuario = (int)jUser["TpUsuario"]; ;
            DtCriacao = (string)jUser["DtCriacao"]; ;
            DtAlteracao = (string)jUser["DtAlteracao"]; ;
            Ativo = (bool)jUser["Ativo"]; ;
        }*/

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Token { get; set; }

        public int TpUsuario { get; set; }

        public string DtCriacao { get; set; }

        public string DtAlteracao { get; set; }

        public bool Ativo { get; set; }

    }
}
