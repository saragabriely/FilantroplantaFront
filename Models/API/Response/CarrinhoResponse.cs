using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filantroplanta.Models.API.Response.Response;
using Filantroplanta.Models.API.Response.Response.Response;
using Filantroplanta.Models.API.Response.Response.Response.Response;

namespace Filantroplanta.Models.API.Response
{
    public class CarrinhoResponse : ResponseBase
    {
        public ItemCarrinho ItemCarrinho { get; set; }
        public v_ItemCarrinho vItemCarrinho { get; set; }
        public List<v_ItemCarrinho> vItensCarrinho { get; set; }
        public List<ItemCarrinho> ItensCarrinho { get; set; }
    }
}
