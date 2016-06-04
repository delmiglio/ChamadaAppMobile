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
using ChamadaAppMobile.Utils.VO;
using Newtonsoft.Json.Linq;

namespace ChamadaAppMobile.Services
{
    public class ConsumeRest
    {
        static readonly string baseURL = "http://192.168.0.11/api/";
        static readonly string getURL = baseURL + "{0}?{1}";
        static readonly string putURL = baseURL + "{0}";

        HttpClient client;

        public ConsumeRest()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Esse método é responsável por realizar requisições com o verbo HttpGet
        /// </summary>
        /// <typeparam name="T">Objeto genérico que será retornado</typeparam>
        /// <param name="controllerAction">a controller com sua a devida action name do recurso desejado</param>
        /// <param name="paramametros">os parametros que o método da controller deverá receber para retornar os dados</param>
        /// <returns>O objeto genérico contendo os dados retornados ou null</returns>
        public async Task<T> GetResponse<T>(string controllerAction, string paramametros) where T : class
        {            
            //Formata a Url com o metodo e o parametro enviado e inicia o acesso a Api. 
            //Await foi utilizado para que o aplicativo não trave qdo estiver aguardano o retorno dos dados.
            //Tornando-o assincrono para que a thread principal continue em execução.
            var response = await client.GetAsync(string.Format(getURL, controllerAction, paramametros));

            var JsonResult = response.Content.ReadAsStringAsync().Result;

            if (typeof(T) == typeof(string))
                return null;           

            if (response.IsSuccessStatusCode)
            {
                var rootobject = JsonConvert.DeserializeObject<T>(JsonResult);
                return rootobject;
            }

            return null;
        }

        /// <summary>
        /// Esse método é responsável por realizar requisições com o verbo HttpPut
        /// </summary>
        /// <typeparam name="T">Objeto genérico que será retornado</typeparam>
        /// <param name="controllerAction">a controller com sua a devida action name do recurso desejado</param>
        /// <param name="obj">o objeto que será realizado o update</param>
        /// <returns>Retorna o objeto devolvido pelo método REST acessado na API, caso o mesmo contenha retorno</returns>
        public async Task<T> PutResponse<T>(string controllerAction, object obj) where T : class
        {
            //No caso de HttpPut, a requisição deverá conter um objeto em formato JSON
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(string.Format(putURL, controllerAction), content);

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

        /// <summary>
        /// Esse método é responsável por realizar requisições com o verbo HttpPost
        /// </summary>
        /// <typeparam name="T">Objeto genérico que será retornado</typeparam>
        /// <param name="controllerAction">a controller com sua a devida action name do recurso desejado</param>
        /// <param name="obj">o objeto que será realizado o post</param>
        /// <returns>Retorna o objeto devolvido pelo método REST acessado na API, caso o mesmo contenha retorno</returns>
        public async Task<T> PostResponse<T>(string controllerAction, object obj) where T : class
        {
            //No caso de HttpPost, a requisição deverá conter um objeto em formato JSON
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(string.Format(putURL, controllerAction), content);

            var JsonResult = response.Content.ReadAsStringAsync().Result;

            if (typeof(T) == typeof(string))
                return null;

            if (response.IsSuccessStatusCode)
            {
                var rootobject = JsonConvert.DeserializeObject<T>(JsonResult);
                return rootobject;
            }

            return null;
        }
    }
}
