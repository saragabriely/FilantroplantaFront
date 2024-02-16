using Filantroplanta.Controle.Produtor;
using Filantroplanta.Models;
using System.Globalization;

namespace Filantroplanta.Views.Restaurante;

public partial class Home : ContentPage
{
    public ControleProduto controleProduto = new ControleProduto();

	public Home()
	{
		InitializeComponent();

        lblMsgInicial.Titulo = "Olá, seja bem-vindo(a)!";
    }

    public void EsconderLista()
    {
        scrollLista.IsVisible   = false;
        lblListaVazia.IsVisible = true;
    }

    private void listaProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var produto = (v_Produto)e.SelectedItem;

        if (produto != null)
            NavegarTelaCadastro(produto);

        ((ListView)sender).SelectedItem = null;
    }

    private async void NavegarTelaCadastro(v_Produto produto) => await Navigation.PushAsync(new VisualizarProduto(produto));

    private void btnPesquisar_Clicked(object sender, EventArgs e) => PesquisarProdutoAPI();

    private async void PesquisarProdutoAPI()
    {
        var produto = etPesquisaProduto.Text;
        
        if(!string.IsNullOrEmpty(produto))
        {
            var produtos = controleProduto.BuscarProdutos(produto);

            if (produtos.vProdutos != null && produtos.vProdutos.Count > 0)
                PopularTela(produtos.vProdutos);
            else
                EsconderLista();
        }
        else
        {
            EsconderLista();
            await DisplayAlert("Produto", "Digite o produto que deseja pesquisar", "OK");
        }
    }

    private void PopularTela(List<v_Produto> produtos)
    {
        listaProdutos.ItemsSource = produtos;
        scrollLista.IsVisible     = true;
        lblListaVazia.IsVisible   = false;
    }
}