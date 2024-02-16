using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class Estoque
    {
        public long EstoqueID { get; set; }
        public long ProdutoID { get; set; }
        public long ProdutorID { get; set; }
        public long QuantidadeDisponivel { get; set; }
        public long QuantidadeVendida { get; set; }
        public long QuantidadeReservada { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}
