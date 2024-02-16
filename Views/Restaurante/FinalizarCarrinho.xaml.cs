using Filantroplanta.Controle;
using Filantroplanta.Controle.Restaurante;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Restaurante;

public partial class FinalizarCarrinho : ContentPage
{
    public ControleUtil controleUtil = new ControleUtil();
    public ControlePedidoRestaurante controlePedido = new ControlePedidoRestaurante();
    private List<v_ItemCarrinho> items;

    public FinalizarCarrinho()
	{
		InitializeComponent();
	}

    public FinalizarCarrinho(List<v_ItemCarrinho> items, Pessoa restaurante)
    {
        InitializeComponent();

        this.items = items;

        listaItensCarrinho.ItemsSource = this.items;
        var soma = this.items.Sum(i => i.ValorTotal);

        lblValorTotal.Text = controleUtil.FormatarDecimal(soma);
        lblEndereco.Text = $"{restaurante.Endereco}, {restaurante.Numero} - {restaurante.Bairro} / {restaurante.Cidade}";
    }

    private async void BtnFinalizar_Clicked(object sender, EventArgs e)
    {
        if(items != null && items.Count > 0)
        {
            var retorno = controlePedido.CriarPedido(items);

            if(retorno)
            {
                await DisplayAlert("Pedido", "Pedido realizado com sucesso!", "OK");
                await Navigation.PopAsync(true);
                await Navigation.PushAsync(new Pedidos());
            }
            else
                await DisplayAlert("Pedido", "Não foi possível fechar o pedido.", "OK");
        }
    }
}