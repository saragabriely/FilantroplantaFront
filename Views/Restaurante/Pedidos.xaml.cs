using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Restaurante;
using Filantroplanta.Models;
using Filantroplanta.Models.API.Pedido;
using System.Globalization;

namespace Filantroplanta.Views.Restaurante;

public partial class Pedidos : ContentPage
{
    public ControlePessoa controlePessoa = new ControlePessoa();
    public Controle.ControlePedido ctrlPedido = new Controle.ControlePedido();
    public ControlePedidoRestaurante controlePedidoR = new ControlePedidoRestaurante();

    public Pedidos()
    {
        InitializeComponent();

        BuscarListaPedidos();
    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));

        BuscarListaPedidos();

        refreshView.IsRefreshing = false;
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

    private void ListaPedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        var pedido = (v_PedidoProdutos)e.SelectedItem;
        if (pedido != null)
            NavegarTelaCadastro(pedido);

        ((ListView)sender).SelectedItem = null;
    }

    private async void NavegarTelaCadastro(v_PedidoProdutos pedido)
    {
        await Navigation.PushAsync(new ItensPedido(pedido.PedidoID));
    }

    private void BuscaStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            var status  = picker.Items[selectedIndex];
            var lista   = ctrlPedido.BuscarListaPedidoAPICliente();
            var pedidos = ctrlPedido.BuscarPedidosPorStatus(status, lista);

            PopularTelaPedidos(pedidos);
        }
    }
}