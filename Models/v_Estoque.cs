using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models
{
    public class v_Estoque
    {
        public Estoque Estoque { get; set; }
        public string NomeProduto { get; set; }
        public Produto Produto { get; set; }
    }
}
