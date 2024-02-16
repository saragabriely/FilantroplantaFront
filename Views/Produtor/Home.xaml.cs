using Filantroplanta.Models;
using Filantroplanta.Mock;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Pedido;

namespace Filantroplanta.Views.Produtor;

public partial class Home : ContentPage
{
    public Controle.ControlePedido ctrlPedido = new Controle.ControlePedido();
    public ControlePessoa ctrlPessoa = new ControlePessoa();

    public Home()
	{
		InitializeComponent();

        PopularLabelBoasVindas();

        BuscarPedidos();
    }

    private void PopularLabelBoasVindas() => lblMsgInicial.Titulo = "Olá, seja bem-vindo(a)!";

    private void Filantroplanta_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Filantro());
    }

    private void BuscarPedidos()
    {
        var produtor = ctrlPessoa.BuscarUsuarioLogado();

        if(produtor != null && produtor.PessoaID > 0)
        {
            var pedidos = ctrlPedido.BuscarListaPedidoAPICache(produtor.PessoaID, Pessoa.Produtor);

            if(pedidos != null && pedidos.Count > 0)
            {
                var lstPendenteAprovacao = FiltrarPedido(pedidos, StatusPedido.Pendente_Aprovacao);
                var lstPendenteEnvio     = FiltrarPedido(pedidos, StatusPedido.Pendente_Envio);
                var lstFinalizados       = FiltrarPedido(pedidos, StatusPedido.Finalizado);
                var lstCancelados        = FiltrarPedido(pedidos, StatusPedido.Cancelado_Cliente);
            
                lblPedidosPendenteAprovacao.Text = lstPendenteAprovacao.Count().ToString();
                lblPendenteEnvio.Text = lstPendenteEnvio.Count().ToString();
                lblFinalizados.Text   = lstFinalizados.Count().ToString();
                lblCancelados.Text    = lstCancelados.Count().ToString();
            }
            else
            {
                lblListaVazia.IsVisible     = true;
                gridResumoPedidos.IsVisible = false;
            }
        }
    }

    public List<PedidoAPI> FiltrarPedido(List<PedidoAPI> pedidos, long statusPedido)
    {
        return pedidos.Where(i => i.Pedido.StatusPedidoID == statusPedido).ToList();
    }
}