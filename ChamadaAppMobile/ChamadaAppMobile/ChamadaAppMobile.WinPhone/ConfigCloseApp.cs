using ChamadaAppMobile.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ConfigCloseApp))]

namespace ChamadaAppMobile.WinPhone
{
    public class ConfigCloseApp : ICloseApp
    {
        public void Close_App()
        {
            System.Windows.Application.Current.Terminate();
        }
    }
}