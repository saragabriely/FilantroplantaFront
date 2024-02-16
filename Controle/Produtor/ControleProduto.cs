using Filantroplanta.API;
using Filantroplanta.Mock;
using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Request;
using Filantroplanta.Models.API.Response;
using LazyCache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle.Produtor
{
    public class ControleProduto : ControleCache
    {
        public ProdutoController produtoController = new ProdutoController();
        private const string const_ChaveListaProdutoProdutor = "ListaProduto_Produtor_";

        public ControleProduto() { }

        public void SalvarAdicionarProduto(ProdutoRequest json, Produto prod, HttpMethod method)
        {
            try
            {
                var retorno = produtoController.CadastroProduto(json, prod, method);

                if(retorno.StatusRetorno == 200)
                {
                    var produto = new Produto();
                        
                    if(method == HttpMethod.Post)
                    {
                        produto = json.Produto;
                        produto.ProdutoID = retorno.ID;
                    }                            
                    else 
                        produto = prod;

                    AdicionarProdutoCache(produto);

                    AdicionarProdutoLista(produto, produto.ProdutorID);

                    var ctrlEstoque = new ControleEstoque();
                    ctrlEstoque.AtualizarListaEstoqueCache(retorno.Estoque, produto.ProdutorID);
                }                
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Produto> ExcluirProduto(Produto produto)
        {
            var retorno       = produtoController.CadastroProduto(null, produto, HttpMethod.Delete);
            var listaProdutos = BuscarListaProdutoCache(produto.ProdutorID);

            if (retorno.StatusRetorno == 200)
            {
                if (listaProdutos != null && listaProdutos.Count > 0)
                {
                    if (listaProdutos.Contains(produto))
                    {
                        listaProdutos.Remove(produto);
                        AtualizarOuCriarLista(listaProdutos, produto.ProdutorID);
                    }
                }
            }

            return listaProdutos;
        }

        public void CriarListaProduto(List<Produto> produtos, long pessoaID) => AtualizarOuCriarLista(produtos, pessoaID);

        public void AdicionarProdutoLista(Produto produto, long pessoaID)
        {
            var lista = BuscarListaProdutoCache(pessoaID);

            if(lista == null)
                lista = new List<Produto>();

            if(!lista.Contains(produto))
                lista.Add(produto);

            AtualizarOuCriarLista(lista, pessoaID);
        }

        public void AtualizarOuCriarLista(List<Produto> lista, long pessoaID)
        {
            cache.GetOrAdd($"{const_ChaveListaProdutoProdutor}{pessoaID}", () =>
            {
                return lista.OrderBy(i => i.Descricao).ToList();
            });
        }

        public List<Produto> BuscarListaProdutoCache(long pessoaID) => cache.Get<List<Produto>>($"{const_ChaveListaProdutoProdutor}{pessoaID}");

        public Produto BuscarProdutoCache(long produtoID, long produtorID)
        {
            var listaProdutos = BuscarListaProdutoCache(produtorID);
            var produto = new Produto();
            
            if(listaProdutos != null && listaProdutos.Count > 0)
                produto = listaProdutos.Where(i => i.ProdutoID == produtoID).FirstOrDefault();

            return produto;
        }

        public void AdicionarProdutoCache(Produto produto)
        {
            var chave = $"Produto_{produto.ProdutoID}";

            cache.GetOrAdd(chave, () =>
            {
                return produto;
            });

            AdicionarChaveCache(chave, produto);
        }

        public ProdutoResponse BuscarProdutos(string produto) => produtoController.BuscarProduto(produto);
    }
}
