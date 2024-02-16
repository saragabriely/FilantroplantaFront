using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Request;
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
    public class ProdutoController
    {
        public ProdutoController() { }

        public static HttpClient client = new HttpClient();
        public static RotasAPIs  rotas  = new RotasAPIs();

        public ProdutoResponse CadastroProduto(ProdutoRequest json, Produto produto, HttpMethod method)
        {
            var t = Task.Run(() => EnviarRequisicaoAPICadastroProduto(json, produto, method));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<ProdutoResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPICadastroProduto(ProdutoRequest json,  Produto produto, HttpMethod method)
        {
            var result = new HttpResponseMessage();
            client = new HttpClient();

            if (method == HttpMethod.Delete)
            {
                client.DefaultRequestHeaders.Add("produtoID", produto.ProdutoID.ToString());
                result = await client.DeleteAsync(rotas.URL_CADASTRO_PRODUTO);
            }
            else
            {
                var jsonProduto = method == HttpMethod.Post ? JsonConvert.SerializeObject(json) : JsonConvert.SerializeObject(produto);

                HttpContent c = new StringContent(jsonProduto, Encoding.UTF8, "application/json");

                if (method == HttpMethod.Post)
                    result = await client.PostAsync(rotas.URL_CADASTRO_PRODUTO, c);
                else if (method == HttpMethod.Put)
                    result = await client.PutAsync(rotas.URL_CADASTRO_PRODUTO, c);
            }

            var retorno = result.Content.ReadAsStringAsync().Result;

            return retorno;
        }

        public ProdutoResponse BuscarProduto(string produto)
        {
            var t = Task.Run(() => EnviarRequisicaoAPIBuscarProduto(produto));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<ProdutoResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPIBuscarProduto(string produto)
        {
            client = new HttpClient();

            if (!client.DefaultRequestHeaders.Contains("nomeProduto"))
                client.DefaultRequestHeaders.Add("nomeProduto", produto);

            var result = await client.GetAsync(rotas.URL_CADASTRO_PRODUTO);

            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
