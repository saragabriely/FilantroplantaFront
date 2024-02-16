using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models.API.Response
{
    public class ProdutoResponse : ResponseBase
    {
        public List<v_Produto> vProdutos { get; set; }
        public Estoque Estoque { get; set; }
    }
}
