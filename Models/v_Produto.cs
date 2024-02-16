using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class v_Produto
    {
        public long ProdutoID { get; set; }
        public string Descricao { get; set; }
        public string NomeProdutor { get; set; }
        public decimal ValorPorKG { get; set; }
        public string ValorPorKGFormatado { get; set; }
        public long ProdutorID { get; set; }
        public long QuantidadeDisponivel { get; set; }
        public string Localizacao { get; set; }
    }
}
