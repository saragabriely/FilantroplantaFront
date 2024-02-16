using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class Pedido
    {
        public long PedidoID { get; set; }
        public long StatusPedidoID { get; set; }
        public decimal ValorTotalPedido { get; set; }
        public long ClienteID { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}
