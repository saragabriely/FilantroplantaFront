using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filantroplanta.Models.API.Response
{
    public class EstoqueResponse : ResponseBase
    {
        public Models.Estoque Estoque { get; set; }
        public List<Models.Estoque> Estoques { get; set; }
    }
}
