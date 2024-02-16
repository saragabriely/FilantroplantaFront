using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filantroplanta.API
{
    public class EstoqueController
    {
        public EstoqueController() { }

        public static HttpClient client = new HttpClient();
        public static RotasAPIs  rotas  = new RotasAPIs();

        public EstoqueResponse AtualizarEstoque(Estoque estoque)
        {
            var t = Task.Run(() => EnviarRequisicaoAPIAtualizarEstoque(estoque));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<EstoqueResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPIAtualizarEstoque(Estoque estoque)
        {
            client = new HttpClient();
            var jsonEstoque = JsonConvert.SerializeObject(estoque);
            HttpContent c = new StringContent(jsonEstoque, Encoding.UTF8, "application/json");
            var    result = await client.PutAsync(rotas.URL_CADASTRO_ESTOQUE, c);

            var retorno = result.Content.ReadAsStringAsync().Result;

            return retorno;
        }

        public EstoqueResponse BuscarEstoquesPorProdutor(long produtorID)
        {
            var t = Task.Run(() => EnviarRequisicaoAPIBuscarEstoque(produtorID));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<EstoqueResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPIBuscarEstoque(long produtorID)
        {
            client = new HttpClient();

            if (!client.DefaultRequestHeaders.Contains("produtorID"))
                client.DefaultRequestHeaders.Add("produtorID", produtorID.ToString());

            var result  = await client.GetAsync(rotas.URL_CADASTRO_ESTOQUE);

            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
