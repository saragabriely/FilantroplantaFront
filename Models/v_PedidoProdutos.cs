using Filantroplanta.Models.API.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class v_PedidoProdutos
    {
        public long PedidoID { get; set; }
        public string StatusPedidoDescricao { get; set; }
        public string DataPedido { get; set; }
        public string ValorTotal { get; set; }
        public PedidoAPI Pedido { get; set; }
    }
}
