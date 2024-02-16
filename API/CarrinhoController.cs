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
    public class CarrinhoController
    {
        public CarrinhoController() { }

        public static HttpClient client = new HttpClient();
        public static RotasAPIs rotas = new RotasAPIs();

        public CarrinhoResponse CadastrarItemCarrinho(ItemCarrinho item, long itemCarrinhoID, HttpMethod method)
        {
            var t = Task.Run(() => EnviarRequisicaoAPICadastroCarrinho(item, itemCarrinhoID, method));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<CarrinhoResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPICadastroCarrinho(ItemCarrinho item, long itemCarrinhoID, HttpMethod method)
        {
            var result = new HttpResponseMessage();
            client = new HttpClient();

            if (method == HttpMethod.Delete)
            {
                client.DefaultRequestHeaders.Add("ItemCarrinhoID", itemCarrinhoID.ToString());
                result = await client.DeleteAsync(rotas.URL_CADASTRO_CARRINHO);
            }
            else
            {
                var jsonProduto = method == HttpMethod.Post ? JsonConvert.SerializeObject(item) : JsonConvert.SerializeObject(item);

                HttpContent c = new StringContent(jsonProduto, Encoding.UTF8, "application/json");

                if (method == HttpMethod.Post)
                    result = await client.PostAsync(rotas.URL_CADASTRO_CARRINHO, c);
                else if (method == HttpMethod.Put)
                    result = await client.PutAsync(rotas.URL_CADASTRO_CARRINHO, c);
            }

            var retorno = result.Content.ReadAsStringAsync().Result;

            return retorno;
        }

        public CarrinhoResponse BuscarItensCarrinhoCliente(long clienteID)
        {
            var t = Task.Run(() => EnviarRequisicaoAPIBuscarItensCarrinhoCliente(clienteID));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<CarrinhoResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPIBuscarItensCarrinhoCliente(long clienteID)
        {
            client = new HttpClient();

            if (!client.DefaultRequestHeaders.Contains("clienteID"))
                client.DefaultRequestHeaders.Add("clienteID", clienteID.ToString());

            var result = await client.GetAsync(rotas.URL_CADASTRO_CARRINHO);

            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
