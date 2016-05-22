using Newtonsoft.Json;
using System;

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
