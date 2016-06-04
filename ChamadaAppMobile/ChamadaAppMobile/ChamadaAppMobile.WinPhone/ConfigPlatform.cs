using SQLite;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using ChamadaAppMobile.WinPhone;
using System;

[assembly: Dependency(typeof(ConfigPlatform))]

namespace ChamadaAppMobile.WinPhone
{
    public class ConfigPlatform : IConfigPlatform
    {
        public SQLiteConnection GetConexao()
        {
            var dbName = "BDMobile.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }

        public void Close_App()
        {
            System.Windows.Application.Current.Terminate();
        }        
    }
}
