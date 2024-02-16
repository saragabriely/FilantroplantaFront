using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filantroplanta.API
{
    public class PessoaController
    {
        public PessoaController() { }

        public static HttpClient client = new HttpClient();
        public static RotasAPIs  rotas  = new RotasAPIs();

        public ResponseBase CadastrarPessoa(Pessoa pessoa)
        {
            var t = Task.Run(() => EnviarRequisicaoAPICadastroPessoa(pessoa));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<ResponseBase>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPICadastroPessoa(Pessoa pessoa)
        {
            var jsonPessoa = JsonConvert.SerializeObject(pessoa);
            HttpContent  c = new StringContent(jsonPessoa, Encoding.UTF8, "application/json");
            HttpResponseMessage result = new HttpResponseMessage();

            client = new HttpClient();

            if (pessoa.PessoaID == 0)
                 result = await client.PostAsync(rotas.URL_CADASTRO_USUARIO, c);
            else
                result  = await client.PutAsync(rotas.URL_CADASTRO_USUARIO, c);

            var retorno = result.Content.ReadAsStringAsync().Result;

            return retorno;
        }

        public PessoaResponse BuscarPessoa(long pessoaID)
        {
            var t = Task.Run(() => EnviarRequisicaoAPIBuscarPessoa(pessoaID));
            t.Wait();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return System.Text.Json.JsonSerializer.Deserialize<PessoaResponse>(t.Result, options);
        }

        private async Task<string> EnviarRequisicaoAPIBuscarPessoa(long pessoaID)
        {
            client = new HttpClient();

            if (!client.DefaultRequestHeaders.Contains("pessoaID"))
                client.DefaultRequestHeaders.Add("pessoaID", pessoaID.ToString());

            var result = await client.GetAsync(rotas.URL_CADASTRO_USUARIO);

            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
