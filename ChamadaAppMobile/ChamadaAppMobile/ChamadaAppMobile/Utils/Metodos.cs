using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChamadaAppMobile.Utils
{
    public class Metodos
    {
        public static T JsonToCustomObject<T>(object obj) where T : class
        {
            string json = JsonConvert.SerializeObject(obj);

            T novoObj = Activator.CreateInstance<T>();

            novoObj = JsonConvert.DeserializeObject<T>(json);

            return novoObj;
        }

        public static List<T> JsonToCustomObject<T>(List<object> listObj) where T : class
        {
            string json = JsonConvert.SerializeObject(listObj);

            List<T> novaLista = Activator.CreateInstance<List<T>>();

            novaLista = JsonConvert.DeserializeObject<List<T>>(json);

            return novaLista;
        }
    }
}
