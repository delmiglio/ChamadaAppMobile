using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChamadaAppMobile.VO;
using ObjCRuntime;


namespace ChamadaAppMobile.Services
{
    public class ConsumeRest
    {
        static readonly string baseURL = "http://192.168.0.11/api/";
        static readonly string getURL = baseURL + "{0}?{1}";
        static readonly string putURL = baseURL + "chamada";

        HttpClient client;

        public ConsumeRest()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            //Definide o Header de resultado para JSON, para evitar que seja retornado um HTML ou XML
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetResponse<T>(string actionController, string paramametros) where T : class
        {            
            //Formata a Url com o metodo e o parametro enviado e inicia o acesso a Api. Como o acesso será por meio
            //da Internet, pode demorar muito, para que o aplicativo não trave usamos um método assincrono
            //e colocamos a keyword AWAIT, para que a Thread principal - UI - continuo sendo executada
            //e o método so volte a ser executado quando o download das informações for finalizado
            var response = await client.GetAsync(string.Format(getURL, actionController, paramametros));

            //Lê a string retornada
            var JsonResult = response.Content.ReadAsStringAsync().Result;

            if (typeof(T) == typeof(string))
                return null;

            //Converte o resultado Json para uma Classe utilizando as Libs do Newtonsoft.Json
            var rootobject = JsonConvert.DeserializeObject<T>(JsonResult);

            return rootobject;
        }

        public async Task<T> PutResponse<T>(string actionController, ChamadaForPresencaVO obj) where T : class
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(putURL, content);

            var JsonResult = response.Content.ReadAsStringAsync().Result;

            if (typeof(T) == typeof(string))
                return null;

            if(response.IsSuccessStatusCode)
            {
                var rootobject = JsonConvert.DeserializeObject<T>(JsonResult);
                return rootobject;
            }

            return null;
        }
    }
}
