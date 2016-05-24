using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Xamarin.Forms;
using ChamadaAppMobile.Droid;
using System.IO;

[assembly: Dependency(typeof(ConfigPlatform))]

namespace ChamadaAppMobile.Droid
{
    public class ConfigPlatform : IConfigPlatform
    {
        public SQLiteConnection GetConexao()
        {
            var dbName = "BDMobile.db3";
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(folder, dbName);
            return new SQLiteConnection(path);
        }

        public void Close_App()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}