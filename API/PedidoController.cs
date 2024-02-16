using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Pedido;
using Filantroplanta.Models.API.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filantroplanta.API
{
    public class PedidoController
    {
        public PedidoController() { }

        public static HttpClient client = new HttpClient();
        public static RotasAPIs rotas = new RotasAPIs();

        public PedidoResponse BuscarPedidos(long clienteID, long produtorID, long pedidoID)
        {
            var t = Task.Run(() => EnviarRequisicaoAPIBuscarPedidos(clienteID, produtorID, pedidoID));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<PedidoResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPIBuscarPedidos(long clienteID, long produtorID, long pedidoID)
        {
            client = new HttpClient();

            if (produtorID > 0 && !client.DefaultRequestHeaders.Contains("produtorID"))
                client.DefaultRequestHeaders.Add("produtorID", produtorID.ToString());

            else if (clienteID > 0 && !client.DefaultRequestHeaders.Contains("clienteID"))
                client.DefaultRequestHeaders.Add("clienteID", clienteID.ToString());

            else if (pedidoID > 0 && !client.DefaultRequestHeaders.Contains("pedidoID"))
                client.DefaultRequestHeaders.Add("pedidoID", pedidoID.ToString());

            var result  = await client.GetAsync(rotas.URL_CADASTRO_PEDIDO);

            return await result.Content.ReadAsStringAsync();
        }

        public PedidoResponse CriarAtualizarPedido(PedidoAPI pedidoAPI, Pedido pedido, HttpMethod method)
        {
            try
            {
                var t = Task.Run(() => EnviarRequisicaoAPICriarAtualizarPedido(pedidoAPI, pedido, method));
                t.Wait();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return System.Text.Json.JsonSerializer.Deserialize<PedidoResponse>(t.Result, options);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> EnviarRequisicaoAPICriarAtualizarPedido(PedidoAPI pedidoAPI, Pedido pedido, HttpMethod method)
        {
            client = new HttpClient();
            var json = "";
            
            if(pedido != null)
                json = JsonConvert.SerializeObject(pedido);
            else
                json = JsonConvert.SerializeObject(pedidoAPI);

            var result = new HttpResponseMessage();
            HttpContent c = new StringContent(json, Encoding.UTF8, "application/json");

            if (method == HttpMethod.Put)
                result = await client.PutAsync(rotas.URL_CADASTRO_PEDIDO, c);
            else
                result = await client.PostAsync(rotas.URL_CADASTRO_PEDIDO, c);

            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
