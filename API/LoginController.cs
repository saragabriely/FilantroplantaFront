using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Mime;
using System.Diagnostics;
using System.Net;
using Filantroplanta.Models;
using Filantroplanta.Models.API.Response.Response.Response.Response.Response;

namespace Filantroplanta.API
{
    public class LoginController
    {
        public LoginController() { }

        public static HttpClient client = new HttpClient();
        public static RotasAPIs rotas = new RotasAPIs();

        public LoginResponse RealizarLogin(string username, string password)
        {
            var t = Task.Run(() => ChamarAPILogin(username, password));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<LoginResponse>(t.Result, options); 
        }

        private async Task<string> ChamarAPILogin(string username, string password)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Email", username);
            client.DefaultRequestHeaders.Add("Senha", password);

            var result  = await client.GetAsync(rotas.URL_LOGIN);
            var retorno = result.Content.ReadAsStringAsync().Result;

            client = new HttpClient();

            return retorno;
        }
    }
}
