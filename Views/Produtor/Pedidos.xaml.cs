using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Models;
using Filantroplanta.Models.API.Pedido;
using System.Globalization;

namespace Filantroplanta.Views.Produtor;

public partial class Pedidos : ContentPage
{
    public ControlePessoa ctrlPessoa = new ControlePessoa();
    public ControlePedido ctrlPedido = new ControlePedido();
    public ControleUtil   ctrlUtil   = new ControleUtil();

    public Pedidos()
    {
        InitializeComponent();

        BuscarListaPedidos();
    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));

        BuscarListaPedidos();
    }

    public void BuscarListaPedidos()
    {
        var pedidos = ctrlPedido.BuscarListaPedidosPessoa();

        PopularTelaPedidos(pedidos);
    }

    private void PopularTelaPedidos(List<PedidoAPI> pedidos)
    {
        if (pedidos != null && pedidos.Count > 0)
        {
            var listaTratada = ctrlPedido.ListaTratadaPedidoProduto(pedidos);

            listaPedidos.ItemsSource = listaTratada;
            listaPedidos.IsVisible = true;
            lblListaVazia.IsVisible = false;
        }
        else
            EsconderLista();
    }

    private void EsconderLista()
    {
        listaPedidos.IsVisible = false;
        lblListaVazia.IsVisible = true;
    }

    private async void ListaPedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        var pedido = (v_PedidoProdutos)e.SelectedItem;
        if (pedido != null && pedido.PedidoID > 0)
        {
            var pedidos   = ctrlPedido.BuscarListaPedidosPessoa(); 
            var pedidoAPI = pedidos.Where(i => i.Pedido.PedidoID == pedido.PedidoID).FirstOrDefault();
            NavegarTelaCadastro(pedidoAPI);
        }
        else
            await DisplayAlert("Vazio", "Pedido vazio", "OK");

        ((ListView)sender).SelectedItem = null;
    }

    private async void NavegarTelaCadastro(PedidoAPI pedido)
    {
        await Navigation.PushAsync(new DetalhePedido(pedido));
    }

    private void BuscaStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            var status  = picker.Items[selectedIndex];
            var lista   = ctrlPedido.BuscarListaPedidoAPIProdutor();
            var pedidos = ctrlPedido.BuscarPedidosPorStatus(status, lista);

            PopularTelaPedidos(pedidos);
        }
    }
}