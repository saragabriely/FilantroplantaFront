using Filantroplanta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filantroplanta.API
{
    public class ViaCepController
    {
        public ViaCepController() { }

        public static HttpClient client = new HttpClient();

        public ViaCepRetorno ChamarAPIViaCep(string cepDigitado)
        {
            var t = Task.Run(() => ChamarViaCep(cepDigitado));
            t.Wait();

            return JsonSerializer.Deserialize<ViaCepRetorno>(t.Result);
        }

        private async Task<string> ChamarViaCep(string cepDigitado)
        {
            client = new HttpClient();
            var url = $"https://viacep.com.br/ws/{cepDigitado}/json/";

            var result = await client.GetAsync(url);
            var retorno = result.Content.ReadAsStringAsync().Result;

            return retorno;
        }
    }
}
