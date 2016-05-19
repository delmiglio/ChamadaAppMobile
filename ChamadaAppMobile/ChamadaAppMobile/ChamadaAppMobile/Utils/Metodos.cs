using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadaAppMobile.Utils
{
    public class Metodos
    {
        public static T JsonToCustomObject<T>(object obj) where T : class
        {
            string jsonUser = JsonConvert.SerializeObject(obj);

            T novoObj = Activator.CreateInstance<T>();

            novoObj = JsonConvert.DeserializeObject<T>(jsonUser);

            return novoObj;
        }
    }
}
