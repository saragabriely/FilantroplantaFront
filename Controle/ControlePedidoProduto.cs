using Filantroplanta.Models;
using Filantroplanta.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Controle
{
    public class ControlePedidoProduto
    {
        public ControlePedidoProduto() { }

        public List<v_PedidoProduto> PopularListaPedidoProduto(List<v_ItemCarrinho> items)
        {
            try
            {
                var pedidoProduto    = new v_PedidoProduto();
                var lstPedidoProduto = new List<v_PedidoProduto>();

                foreach (var item in items)
                {
                    pedidoProduto = new v_PedidoProduto
                    {
                        PedidoProdutoID = 0,
                        PedidoID = 0,
                        ProdutoID = item.ProdutoID,
                        Quantidade = item.Quantidade,
                        ValorPorKg = Convert.ToDecimal(item.ValorPorKG.Replace("R$ ", "").Replace(",", ".")),
                        ValorTotalProduto = item.ValorTotal,
                        Local = item.Localizacao,
                        NomeProdutor = item.NomeProdutor
                    };

                    lstPedidoProduto.Add(pedidoProduto);
                }

                return lstPedidoProduto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
