using Filantroplanta.API;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Models;
using Filantroplanta.Models.API.Pedido;
using Filantroplanta.Models.API.Response;
using Filantroplanta.Views.Produtor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle.Restaurante
{
    public class ControlePedidoRestaurante : ControlePedido
    {
        public PedidoController pessoaController = new PedidoController();

        public ControlePessoa        ctrlPessoa     = new ControlePessoa();
        public ControlePedidoProduto ctrlPedidoProd = new ControlePedidoProduto();
        public ControleCarrinho      ctrlCarrinho   = new ControleCarrinho();
        
        public ControlePedidoRestaurante() { }

        public bool CriarPedido(List<v_ItemCarrinho> items)
        {
            var retorno = true;
            var cliente = ctrlPessoa.BuscarUsuarioLogado();

            try
            {
                var pedido = new Pedido
                {
                    StatusPedidoID   = StatusPedido.Pendente_Aprovacao,
                    ValorTotalPedido = items.Sum(i => i.ValorTotal),
                    ClienteID        = cliente.PessoaID
                };

                var lstPedidoProduto = ctrlPedidoProd.PopularListaPedidoProduto(items);

                var json = new PedidoAPI
                {
                    Pedido   = pedido,
                    Produtos = lstPedidoProduto
                };

                if (pedido != null && lstPedidoProduto != null && lstPedidoProduto.Count > 0)
                {
                    var retornoAPI = pessoaController.CriarAtualizarPedido(json, null, HttpMethod.Post);

                    if (retornoAPI != null && retornoAPI.StatusRetorno == (int)HttpStatusCode.OK)
                    {
                        ctrlCarrinho.AtualizarListaItemCarrinho(retornoAPI.vItensCarrinho, cliente.PessoaID);

                        AtualizarListaPedidoCache(retornoAPI, cliente);

                        retorno = true;
                    }
                    else
                        retorno = false;
                }
            }
            catch (Exception)
            {
                retorno = false;
                throw;
            }

            return retorno;
        }

        private void AtualizarListaPedidoCache(PedidoResponse response, Models.Pessoa cliente)
        {
            var listaPedido = new List<PedidoAPI>();
            var pedidos     = BuscarListaPedidosPessoa(cliente);

            if(pedidos != null && pedidos.Count > 0)
            {
                if(!pedidos.Contains(response.PedidoAPI))
                {
                    pedidos.Add(response.PedidoAPI);
                    listaPedido = pedidos;
                }
            }
            else
                listaPedido.Add(response.PedidoAPI);

            CriarOuAtualizarListaPedidoCache(listaPedido, cliente.PessoaID, cliente.TipoPessoaID);
        }
    }
}
 