using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class PedidoProduto
    {
        public long PedidoProdutoID { get; set; }
        public long PedidoID { get; set; }
        public long ProdutoID { get; set; }
        public long Quantidade { get; set; }
        public decimal ValorPorKg { get; set; }
        public decimal ValorTotalPedido { get; set; }
        public int StatusPedidoProdutoID { get; set; }
        public long ProdutorID { get; set; }

        public const int ProdutorPendenteAprovacao = 1;
        public const int ProdutorAprovado          = 2;
        public const int ProdutorCancelado         = 3;
        public const int ProdutorPendenteEnvio     = 4;
    }
}
