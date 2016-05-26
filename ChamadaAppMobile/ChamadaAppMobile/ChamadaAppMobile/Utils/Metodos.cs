using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public static ObservableCollection<T> ListToObservableCollection<T>(List<T> lista) where T : class
        {
            ObservableCollection<T> novaLista = Activator.CreateInstance<ObservableCollection<T>>();

            foreach(T obj in lista)
            {
                novaLista.Add(obj);
            }

            return novaLista;
        }
    }
}
