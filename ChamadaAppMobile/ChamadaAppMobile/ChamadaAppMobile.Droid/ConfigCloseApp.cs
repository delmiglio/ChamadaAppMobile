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
using Xamarin.Forms;
using ChamadaAppMobile.Droid;

[assembly: Dependency(typeof(ConfigCloseApp))]

namespace ChamadaAppMobile.Droid
{
    public class ConfigCloseApp : ICloseApp
    {
        public void Close_App()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}