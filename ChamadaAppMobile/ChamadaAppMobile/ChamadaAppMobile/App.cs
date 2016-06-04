using ChamadaApp.Api.Utils;
using ChamadaAppMobile.Forms;
using ChamadaAppMobile.Services;
using ChamadaAppMobile.Utils.DAO;
using ChamadaAppMobile.Utils.Enum;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChamadaAppMobile
{
    public class App : Application
    {
        static UsuarioDAO dataBase;        
        
        public static UsuarioDAO DataBase
        {
            get
            {
                if (dataBase == null)
                    dataBase = new UsuarioDAO();

                return dataBase;
            }
        }

        public App()
        {
            GetHome();
        }        

        public static void GetHome()
        {
            VO.UsuarioVO usuario = DataBase.GetUniqueUser();
            ContentPage paginaRetorno = null;

            if (usuario != null)
            {
                if ((TpUsuarioEnum)usuario.TpUsuario == TpUsuarioEnum.Aluno)
                    paginaRetorno = new ContentPageHomeAluno();
                else
                    paginaRetorno = new ContentPageHomeProfessor();
            }
            else
                paginaRetorno = new ContentPageLogin();

             App.Current.MainPage = paginaRetorno;
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
