using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class v_ItemCarrinho
    {
        public long ItemCarrinhoID { get; set; }
        public long ProdutoID { get; set; }
        public string NomeProduto { get; set; }
        public long ProdutorID { get; set; }
        public string NomeProdutor { get; set; }
        public long ClienteID { get; set; }
        public long Quantidade { get; set; }
        public string ValorPorKG { get; set; }
        public decimal ValorTotal { get; set; }
        public string ValorTotalFormatado { get; set; }
        public string Localizacao { get; set; }
    }
}
