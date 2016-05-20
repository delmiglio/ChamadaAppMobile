using ChamadaApp.Domain.VO;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChamadaAppMobile.Utils.DAO
{
    public class UsuarioDAO
    {
        protected static object locker = new object();
        protected SQLiteConnection dataBase;

        public UsuarioDAO()
        {
            dataBase = DependencyService.Get<IConfigDB>().GetConexao();
            dataBase.CreateTable<UsuarioVO>();
        }

        public IEnumerable<UsuarioVO> GetUsuarios()
        {
            lock (locker)
            {
                return (from i in dataBase.Table<UsuarioVO>() select i).ToList();
            }
        }

        public UsuarioVO GetUsuario(int usuarioId)
        {
            lock (locker)
            {
                return dataBase.Table<UsuarioVO>().FirstOrDefault(x => x.Id == usuarioId);
            }
        }

        public UsuarioVO GetUniqueUser()
        {
            lock (locker)
            {
                return dataBase.Query<UsuarioVO>("SELECT * FROM UsuarioVO ORDER BY Id ASC LIMIT 1").First<UsuarioVO>();
            }
        }

        public int SaveUsuario(UsuarioVO usuario)
        {
            lock (locker)
            {
                if (dataBase.Insert(usuario) != 0)
                {
                    dataBase.Update(usuario);
                }

                return usuario.Id;
            }
        }

        public int DeleteUsuario(int usuarioId)
        {
            lock (locker)
            {
                return dataBase.Delete<UsuarioVO>(usuarioId);
            }
        }

        public void DeleteAll()
        {
            lock (locker)
            {
                dataBase.DropTable<UsuarioVO>();
                dataBase.CreateTable<UsuarioVO>();
            }
        }
    }
}
