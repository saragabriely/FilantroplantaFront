using Filantroplanta.API;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Models;
using Filantroplanta.Models.API.Pedido;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle
{
    public class ControlePedido : ControleCache
    {
        public PedidoController pedidoController = new PedidoController();
        public ControlePessoa   ctrlPessoa       = new ControlePessoa();
        public ControleUtil     ctrlUtil         = new ControleUtil();

        public ControlePedido() { }

        public string GerarChaveLista(int tipoPessoaID)
        {
            return tipoPessoaID == Models.Pessoa.Produtor ? "ListaPedido_Produtor_" : "ListaPedido_Cliente_";
        }

        public bool AtualizarPedido(Pedido pedido, HttpMethod method, long pessoaID, int tipoPessoa)
        {
            var retorno = pedidoController.CriarAtualizarPedido(null, pedido, method);

            if (method == HttpMethod.Put && retorno.StatusRetorno == (int)HttpStatusCode.OK)
                AtualizarStatusPedidoCache(pedido, pessoaID, tipoPessoa);

            return retorno.StatusRetorno == (int)HttpStatusCode.OK ? true : false;
        }

        private void AtualizarStatusPedidoCache(Pedido pedidoAtualizado, long pessoaID, int tipoPessoa)
        {
            var listaCache = BuscarListaPedidoAPICache(pessoaID, tipoPessoa);

            if(listaCache != null && listaCache.Count > 0)
            {
                var pedidoAux  = listaCache.Where(i => i.Pedido.PedidoID == pedidoAtualizado.PedidoID).FirstOrDefault(); //BuscarPedidoAPICache(pedido.PedidoID, pessoaID, tipoPessoa);

                if (pedidoAux != null && pedidoAux.Pedido != null && pedidoAux.Pedido.PedidoID > 0)
                {
                    // remove o pedido com status antigo
                    listaCache.Remove(pedidoAux);

                    // adiciona o pedido com status atualizado
                    pedidoAux.Pedido = pedidoAtualizado;
                    listaCache.Add(pedidoAux);

                    CriarOuAtualizarListaPedidoCache(listaCache, pessoaID, tipoPessoa);
                }
            }
        }

        public void CriarOuAtualizarListaPedidoCache(List<PedidoAPI> lista, long pessoaID, int tipoPessoaID)
        {
            foreach (var item in lista)
            {
                item.Pedido.PedidoID = item.Pedido.PedidoID;
            };

            var chave = $"{GerarChaveLista(tipoPessoaID)}{pessoaID}";

            RemoverChaveCache(chave);

            cache.GetOrAdd(chave, () =>
            {
                return lista;
            });

            AdicionarChaveCache(chave, lista);
        }

        public PedidoAPI BuscarPedidoAPICache(long pedidoID, long pessoaID, int tipoPessoaID)
        {
            var retorno = new PedidoAPI();
            var lista   = BuscarListaPedidoAPICache(pessoaID, tipoPessoaID);

            if(lista != null && lista.Count > 0)
            {
                var pedidoAux = lista.Where(i => i.Pedido.PedidoID == pedidoID).FirstOrDefault();

                if (pedidoAux != null && pedidoAux.Pedido != null && pedidoAux.Pedido.PedidoID > 0)
                    retorno = pedidoAux;
            }

            return retorno;
        }

        public List<PedidoAPI> BuscarListaPedidoAPICache(long pessoaID, int tipoPessoaID)
        {
            var chave = $"{GerarChaveLista(tipoPessoaID)}{pessoaID}";

            var retorno = cache.Get<List<PedidoAPI>>(chave);

            return retorno != null ? retorno.OrderByDescending(x => x.Pedido.DataCadastro).ToList() : new List<PedidoAPI>();
        }

        public List<PedidoAPI> BuscarListaPedidoAPICliente()
        {
            var cliente = ctrlPessoa.BuscarUsuarioLogado();

            var pedidos = BuscarPedidosAPICliente(cliente.PessoaID);

            CriarOuAtualizarListaPedidoCache(pedidos, cliente.PessoaID, cliente.TipoPessoaID);

            return pedidos != null ? pedidos.OrderByDescending(x => x.Pedido.DataCadastro).ToList() : new List<PedidoAPI>();
        }

        public List<PedidoAPI> BuscarPedidosAPICliente(long clienteID)
        {
            var listaRetorno = new List<PedidoAPI>();

            if (clienteID > 0)
            {
                var retornoAPI = pedidoController.BuscarPedidos(clienteID, 0, 0);

                if (retornoAPI != null && retornoAPI.StatusRetorno == (int)HttpStatusCode.OK && retornoAPI.Pedidos != null && retornoAPI.Pedidos.Count() > 0)
                {
                    listaRetorno = retornoAPI.Pedidos;
                }
            }

            return listaRetorno;
        }

        public List<PedidoAPI> BuscarListaPedidoAPIProdutor()
        {
            var produtor = ctrlPessoa.BuscarUsuarioLogado();

            var pedidos = BuscarPedidosAPIProdutor(produtor.PessoaID);

            CriarOuAtualizarListaPedidoCache(pedidos, produtor.PessoaID, produtor.TipoPessoaID);

            return pedidos != null ? pedidos.OrderByDescending(x => x.Pedido.DataCadastro).ToList() : new List<PedidoAPI>();
        }

        public List<PedidoAPI> BuscarPedidosAPIProdutor(long produtorID)
        {
            var listaRetorno = new List<PedidoAPI>();

            if (produtorID > 0)
            {
                var retornoAPI = pedidoController.BuscarPedidos(0, produtorID, 0);

                if (retornoAPI != null && retornoAPI.StatusRetorno == (int)HttpStatusCode.OK && retornoAPI.Pedidos != null && retornoAPI.Pedidos.Count() > 0)
                {
                    listaRetorno = retornoAPI.Pedidos;
                }
            }

            return listaRetorno;
        }

        public List<PedidoAPI> BuscarListaPedidosPessoa(Models.Pessoa pessoa = null)
        {
            if(pessoa == null)
                pessoa = ctrlPessoa.BuscarUsuarioLogado();

            return BuscarListaPedidoAPICache(pessoa.PessoaID, pessoa.TipoPessoaID);
        }

        public List<PedidoAPI> FiltrarPedidosPorStatus(List<PedidoAPI> pedidos, int status)
        {
            return pedidos.Where(i => i.Pedido.StatusPedidoID == status).ToList();
        }

        public List<PedidoAPI> BuscarPedidosPorStatus(string status, List<PedidoAPI> pedidos)
        {
            if (status.Equals(StatusPedido.string_Pendente_Aprovacao))
                return FiltrarPedidosPorStatus(pedidos, StatusPedido.Pendente_Aprovacao);

            else if (status.Equals(StatusPedido.string_Pendente_Envio))
                return FiltrarPedidosPorStatus(pedidos, StatusPedido.Pendente_Envio);

            else if (status.Equals(StatusPedido.string_Finalizado))
                return FiltrarPedidosPorStatus(pedidos, StatusPedido.Finalizado);

            else if (status.Equals(StatusPedido.string_Cancelado_Produtor))
                return FiltrarPedidosPorStatus(pedidos, StatusPedido.Cancelado_Produtor);

            else if (status.Equals(StatusPedido.string_Cancelado_Cliente))
                return FiltrarPedidosPorStatus(pedidos, StatusPedido.Cancelado_Cliente);

            else
                return pedidos;
        }

        public List<v_PedidoProdutos> ListaTratadaPedidoProduto(List<PedidoAPI> pedidos)
        {
            var lista = new List<v_PedidoProdutos>();
            var aux = new v_PedidoProdutos();

            foreach (var pedido in pedidos)
            {
                aux = new v_PedidoProdutos
                {
                    PedidoID    = pedido.Pedido.PedidoID,
                    DataPedido  = pedido.Pedido.DataCadastro.ToString("dd/MM/yyyy"),
                    StatusPedidoDescricao = BuscarStatusPedido(pedido.Pedido.StatusPedidoID),
                    ValorTotal  = ctrlUtil.FormatarDecimal(pedido.Produtos.Sum(i => i.ValorTotalProduto))
                };

                lista.Add(aux);
            }

            return lista;
        }

        public string BuscarStatusPedido(long id)
        {
            if (id == StatusPedido.Pendente_Aprovacao) return "Pendente de Aprovação";
            else if (id == StatusPedido.Pendente_Envio) return "Pendente de Envio";
            else if (id == StatusPedido.Finalizado) return "Finalizado";
            else if (id == StatusPedido.Cancelado_Cliente) return "Cancelado pelo cliente";
            else return "Cancelado pelo produtor";
        }
    }
}
