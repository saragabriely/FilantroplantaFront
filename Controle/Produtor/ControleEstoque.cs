using Filantroplanta.API;
using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Response;
using LazyCache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle.Produtor
{
    public class ControleEstoque : ControleCache
    {
        public ControleProduto   ctrlProduto       = new ControleProduto();
        public EstoqueController estoqueController = new EstoqueController();

        public ControleEstoque() { }

        public List<Estoque> BuscarEstoquePorProdutor(long produtorID)
        {
            var estoques = new List<Estoque>();

            var retornoAPI = estoqueController.BuscarEstoquesPorProdutor(produtorID);

            if (retornoAPI.Estoques != null)
                estoques = retornoAPI.Estoques;

            return estoques;
        }

        public EstoqueResponse AtualizarEstoque(Estoque estoque)
        {
            var response = estoqueController.AtualizarEstoque(estoque);

            if(response != null && response.StatusRetorno == (int)HttpStatusCode.OK)
            {
                var listaAtual = BuscarListaEstoqueCache(estoque.ProdutorID);

                if(listaAtual != null && listaAtual.Count > 0)
                {
                    foreach(var item in listaAtual)
                    {
                        if (item.EstoqueID == estoque.EstoqueID)
                            item.QuantidadeDisponivel = estoque.QuantidadeDisponivel;
                    }

                    AtualizarOuCriarListaEstoque(listaAtual, estoque.ProdutorID);
                }
            }

            return response;
        }

        public void AtualizarOuCriarListaEstoque(List<Estoque> lista, long pessoaID)
        {
            var chave = $"ListaEstoque_Produtor_{pessoaID}";

            cache.GetOrAdd(chave, () =>
            {
                return lista;
            });

            AdicionarChaveCache(chave, lista);
        }

        public void AtualizarListaEstoqueCache(Estoque estoque, long pessoaID)
        {
            var listaEstoque = BuscarListaEstoqueCache(pessoaID);
        
            listaEstoque.Add(estoque);
        
            AtualizarOuCriarListaEstoque(listaEstoque, pessoaID);
        }

        public List<Estoque> BuscarListaEstoqueCache(long pessoaID)
        {
            return cache.Get<List<Estoque>>($"ListaEstoque_Produtor_{pessoaID}");
        }

        public List<v_Estoque> PopularVEstoque(List<Estoque> estoques)
        {
            var lista       = new List<v_Estoque>();
            var nomeProduto = "";
            var produto     = new Produto();

            foreach (var estoque in estoques)
            {
                produto = ctrlProduto.BuscarProdutoCache(estoque.ProdutoID, estoque.ProdutorID);
                nomeProduto = produto != null && produto.ProdutoID > 0 ? produto.Descricao : "";

                lista.Add(new v_Estoque()
                {
                    Estoque     = estoque,
                    NomeProduto = produto.Descricao,
                    Produto     = produto
                });
            }

            return lista != null && lista.Count > 0 ? lista.OrderBy(i => i.NomeProduto).ToList() : lista;
        }

        //public void RemoverEstoqueCache(long produtoID, long produtorID)
        //{
        //    var lstEstoqueCache = BuscarListaEstoqueCache(produtorID);
        //
        //    if(lstEstoqueCache != null && lstEstoqueCache.Count > 0)
        //    {
        //        var estoque = lstEstoqueCache.Where(i => i.ProdutoID == produtoID).FirstOrDefault();
        //
        //        if(estoque != null && estoque.EstoqueID > 0)
        //        {
        //            lstEstoqueCache.Remove(estoque);
        //
        //            AtualizarOuCriarListaEstoque(lstEstoqueCache, produtorID);
        //        }
        //    }
        //}
    }
}
