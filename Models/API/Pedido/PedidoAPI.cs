using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models.API.Pedido
{
    public class PedidoAPI
    {
        public Models.Pedido Pedido { get; set; }
        public List<v_PedidoProduto> Produtos { get; set; }
    }
}
