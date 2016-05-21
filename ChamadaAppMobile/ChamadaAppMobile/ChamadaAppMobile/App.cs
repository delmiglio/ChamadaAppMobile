using ChamadaAppMobile.Forms;
using ChamadaAppMobile.Utils.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private ContentPage GetHome()
        {
            if (App.DataBase.GetUniqueUser() != null)
                return new ContentPageHome();
            else
                return new ContentPageLogin();
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
