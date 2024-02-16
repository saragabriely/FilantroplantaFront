using Filantroplanta.API;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Controle.Restaurante;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle
{
    public class ControleLogin : ControleCache
    {
        public ControleProduto controleProduto = new ControleProduto();
        public ControleEstoque controleEstoque = new ControleEstoque();
        public ControlePessoa  controlePessoa  = new ControlePessoa();
        public ControlePedido  controlePedido  = new ControlePedido();
        public ControleCarrinho controleCarrinho = new ControleCarrinho();

        public ControleLogin() { }

        public string ExecutarLogin(string login, string senha)
        {
            LoginController loginController = new LoginController();

            var loginResultado = loginController.RealizarLogin(login, senha);

            if (loginResultado.StatusRetorno == (int)HttpStatusCode.OK && loginResultado.Pessoa != null)
            {
                InicializaPropriedadesCache();

                controlePessoa.AdicionarSalvarPessoaCache(loginResultado.Pessoa, $"Pessoa_{loginResultado.Pessoa.PessoaID}");

                if (loginResultado.Produtos != null && loginResultado.Produtos.Count > 0)
                    controleProduto.CriarListaProduto(loginResultado.Produtos, loginResultado.Pessoa.PessoaID);

                if (loginResultado.Estoques != null && loginResultado.Estoques.Count > 0)
                    controleEstoque.AtualizarOuCriarListaEstoque(loginResultado.Estoques, loginResultado.Pessoa.PessoaID);

                if (loginResultado.Pedidos != null && loginResultado.Pedidos.Count > 0)
                    controlePedido.CriarOuAtualizarListaPedidoCache(loginResultado.Pedidos, loginResultado.Pessoa.PessoaID, loginResultado.Pessoa.TipoPessoaID);

                if(loginResultado.Pessoa.TipoPessoaID == Models.Pessoa.Restaurante && loginResultado.vItensCarrinho != null && loginResultado.vItensCarrinho.Count > 0)
                    controleCarrinho.AtualizarOuCriarListaCarrinho(loginResultado.vItensCarrinho, loginResultado.Pessoa.PessoaID);

                if (loginResultado.Pessoa.TipoPessoaID == Models.Pessoa.Produtor)
                    return "Produtor";
                else
                    return "Restaurante";
            }
            else
                return loginResultado.Erro;
        }
    }
}
