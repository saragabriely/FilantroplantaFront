using System.Threading.Tasks;
using Filantroplanta.Models.API.Pedido;
using Filantroplanta.Models.API.Response.Response;
using Filantroplanta.Models.API.Response.Response.Response;
using Filantroplanta.Models.API.Response.Response.Response.Response;

namespace Filantroplanta.Models.API.Response
{
    public class PedidoResponse : ResponseBase
    {
        public Models.Pedido Pedido { get; set; }
        public PedidoAPI PedidoAPI { get; set; }
        public List<PedidoAPI> Pedidos { get; set; }
        public List<v_ItemCarrinho> vItensCarrinho { get; set; }
    }
}
