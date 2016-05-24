using ChamadaAppMobile.Forms;
using ChamadaAppMobile.Utils.DAO;
using ChamadaAppMobile.Utils.Enum;

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
            MainPage = GetHome();
        }

        public static Page GetHome()
        {
            VO.UsuarioVO usuario = DataBase.GetUniqueUser();
            ContentPage paginaRetorno = null;

            if (usuario != null)
            {
                if ((TpUsuarioEnum)usuario.TpUsuario == TpUsuarioEnum.Aluno)
                    paginaRetorno = new ContentPageHomeAluno();
                else
                    return paginaRetorno = new ContentPageHomeProfessor();
            }                
            else
                paginaRetorno = new ContentPageLogin();

            return paginaRetorno;
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
