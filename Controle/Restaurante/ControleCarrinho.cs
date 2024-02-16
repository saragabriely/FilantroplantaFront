using Filantroplanta.API;
using Filantroplanta.Models;
using LazyCache;
using System.Net;

namespace Filantroplanta.Controle.Restaurante
{
    public class ControleCarrinho
    {
        public readonly IAppCache cache = new CachingService();
        public CarrinhoController controller = new CarrinhoController();
        public const string ChaveListaCarrinho = "ListaItemCarrinho_";
        public ControleCarrinho() { }

        public bool SalvarAdicionarCarrinho(ItemCarrinho item, long idRestaurante)
        {
            var retorno = controller.CadastrarItemCarrinho(item, 0, HttpMethod.Post);

            if (retorno != null && retorno.StatusRetorno == (int)HttpStatusCode.OK)
            {
                var lista = BuscarListaCarrinhoCache(idRestaurante);

                AdicionarItemCarrinhoListaCache(retorno.vItemCarrinho, idRestaurante, lista);

                return true;
            }
            else
                return false;
        }

        public List<v_ItemCarrinho> BuscarListaItemCarrinhoCache(long idRestaurante)
        {
            return cache.Get<List<v_ItemCarrinho>>($"{ChaveListaCarrinho}{idRestaurante}");
        }

        public void AdicionarItemCarrinhoListaCache(v_ItemCarrinho item, long idRestaurante, List<v_ItemCarrinho> lista)
        {
            try
            {
                if (lista.Count == 0)
                    lista.Add(item);
                else
                {
                    var validaItemExistente = lista.Where(i => i.ItemCarrinhoID == item.ItemCarrinhoID).FirstOrDefault();

                    if (validaItemExistente != null && validaItemExistente.ItemCarrinhoID > 0)
                    {
                        lista.Remove(validaItemExistente);
                        lista.Add(item);
                    }
                    else
                    {
                        lista.Add(item);
                    }
                }

                AtualizarListaItemCarrinho(lista, idRestaurante);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void AtualizarOuCriarListaCarrinho(List<v_ItemCarrinho> vItensCarrinho, long pessoaID)
        {
            cache.GetOrAdd($"{ChaveListaCarrinho}{pessoaID}", () =>
            {
                return vItensCarrinho;
            });
        }

        public List<v_ItemCarrinho> BuscarListaCarrinhoCache(long pessoaID)
        {
            List<v_ItemCarrinho> lista = cache.Get<List<v_ItemCarrinho>>($"{ChaveListaCarrinho}{pessoaID}");

            return lista != null && lista.Count > 0 ? lista : new List<v_ItemCarrinho>();
        }

        public void AtualizarListaItemCarrinho(List<v_ItemCarrinho> lista, long idRestaurante)
        {
            var chave = $"{ChaveListaCarrinho}{idRestaurante}";

            if (lista != null && lista.Count == 0)
                cache.Remove(chave);
            else
            {
                cache.Remove(chave);
                cache.GetOrAdd(chave, () => { return lista; });
            }
        }

        public bool ExcluirItemCarrinho(v_ItemCarrinho item, long idRestaurante)
        {
            var retorno = false;
            var retornoAPI = controller.CadastrarItemCarrinho(null, item.ItemCarrinhoID, HttpMethod.Delete);

            if (retornoAPI != null && retornoAPI.StatusRetorno == (int)HttpStatusCode.OK)
            {
                var listaItemCarrinho = BuscarListaItemCarrinhoCache(idRestaurante);

                if (listaItemCarrinho.Contains(item))
                {
                    listaItemCarrinho.Remove(item);
                    AtualizarListaItemCarrinho(listaItemCarrinho, idRestaurante);

                    retorno = true;
                }
            }

            return retorno;
        }

        public void ExcluirItemCarrinhoCache(v_ItemCarrinho item, long idRestaurante)
        {
            var listaItemCarrinho = BuscarListaItemCarrinhoCache(idRestaurante);

            if (listaItemCarrinho.Contains(item))
            {
                listaItemCarrinho.Remove(item);
                AtualizarListaItemCarrinho(listaItemCarrinho, idRestaurante);
            }
        }

        public bool InserirItemCarrinho(Models.Pessoa restaurante, v_Produto produto, int quantidade)
        {
            try
            {
                var itemCarrinho = new ItemCarrinho();

                itemCarrinho.ClienteID = restaurante.PessoaID;
                itemCarrinho.Quantidade = quantidade;
                itemCarrinho.ProdutoID = produto.ProdutoID;
                itemCarrinho.ProdutorID = produto.ProdutorID;
                itemCarrinho.ValorPorKG = produto.ValorPorKG;
                itemCarrinho.ValorTotal = quantidade * produto.ValorPorKG;
                itemCarrinho.DataAdicaoItem = DateTime.Now;
                itemCarrinho.Localizacao = produto.Localizacao;

                return SalvarAdicionarCarrinho(itemCarrinho, restaurante.PessoaID);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
