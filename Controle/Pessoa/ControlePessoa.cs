using Filantroplanta.API;
using Filantroplanta.Mock;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle.Pessoa
{
    public class ControlePessoa : ControleCache
    {
        public PessoaController pessoaController = new PessoaController();
        public void Logout()
        {
            LimparCache();
        }

        public Models.Pessoa BuscarPessoa(long pessoaID)
        {
            var pessoa = new Models.Pessoa();

            var retornoAPI  = pessoaController.BuscarPessoa(pessoaID);
            if (retornoAPI != null && retornoAPI.StatusRetorno == (int)HttpStatusCode.OK && retornoAPI.Pessoa != null && retornoAPI.Pessoa.PessoaID > 0)
                pessoa = retornoAPI.Pessoa;

            return pessoa; 
        }

        public void AdicionarSalvarPessoaCache(Models.Pessoa pessoa, string chave)
        {
            Instance.pessoa = pessoa;
            AdicionarChaveCache(chave, pessoa);

            cache.GetOrAdd(chave, () =>
            {
                return pessoa;
            });
        }

        public Models.Pessoa BuscarUsuarioLogado()
        {
            return Instance.pessoa;
        }
    }
}
