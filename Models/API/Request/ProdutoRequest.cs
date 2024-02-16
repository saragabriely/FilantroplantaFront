using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models.API.Request
{
    public class ProdutoRequest
    {
        public Produto Produto { get; set; }
        public long Quantidade { get; set; }
    }
}
