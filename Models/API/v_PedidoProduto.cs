using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models.API
{
    public class v_PedidoProduto
    {
        public long PedidoID { get; set; }
        public long PedidoProdutoID { get; set; }
        public long ProdutoID { get; set; }
        public long StatusPedidoID { get; set; }
        public long ClienteID { get; set; }
        public string NomeProdutor { get; set; }
        public string NomeCliente { get; set; }
        public string DescricaoProduto { get; set; }
        public long Quantidade { get; set; }
        public decimal ValorPorKg { get; set; }
        public decimal ValorTotalProduto { get; set; }
        public string Local { get; set; }
    }
}
