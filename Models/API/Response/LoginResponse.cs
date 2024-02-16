using Filantroplanta.Models.API.Pedido;
using Filantroplanta.Models.API.Response.Response;
using Filantroplanta.Models.API.Response.Response.Response;
using Filantroplanta.Models.API.Response.Response.Response.Response;

namespace Filantroplanta.Models.API.Response.Response.Response.Response.Response
{
    public class LoginResponse : ResponseBase
    {
        public Pessoa Pessoa { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<Models.Estoque> Estoques { get; set; }
        public List<PedidoAPI> Pedidos { get; set; }
        public List<v_ItemCarrinho> vItensCarrinho { get; set; }
    }
}
