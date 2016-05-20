using Newtonsoft.Json.Linq;
using SQLite;
using System;

namespace ChamadaApp.Domain.VO
{
    public class UsuarioVO
    {
        public UsuarioVO()
        {

        }
        
        [PrimaryKey]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(200)]
        public string Sobrenome { get; set; }

        [MaxLength(20)]
        public string Login { get; set; }

        [MaxLength(20)]
        public string Senha { get; set; }

        [MaxLength(200)]
        public string Token { get; set; }

        public int TpUsuario { get; set; }

        public string DtCriacao { get; set; }

        public string DtAlteracao { get; set; }

        public bool Ativo { get; set; }

    }
}
