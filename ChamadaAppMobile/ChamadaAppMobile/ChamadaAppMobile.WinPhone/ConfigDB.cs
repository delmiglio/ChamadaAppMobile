using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using ChamadaAppMobile.WinPhone;

[assembly: Dependency(typeof(ConfigDB))]

namespace ChamadaAppMobile.WinPhone
{
    public class ConfigDB : IConfigDB
    {
        public SQLiteConnection GetConexao()
        {
            var dbName = "BDTCCMobile.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);

            return new SQLiteConnection(path);
        }
    }
}
