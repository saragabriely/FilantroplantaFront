using Filantroplanta.Models;
using Filantroplanta.Mock;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Controle.Pessoa;

namespace Filantroplanta.Views.Produtor;

public partial class Produtos : ContentPage
{
    public ControleProduto ctrlProduto = new ControleProduto();
    public ControlePessoa  ctrlPessoa  = new ControlePessoa();

	public Produtos()
	{
		InitializeComponent();

        var produtor = ctrlPessoa.BuscarUsuarioLogado();

        BuscarProdutos(produtor.PessoaID);
    }

    public Produtos(List<Produto> produtos)
    {
        InitializeComponent();

        ValidarLista(produtos);      
    }

    private void BuscarProdutos(long pessoaID)
    {
        var lista = ctrlProduto.BuscarListaProdutoCache(pessoaID);

        ValidarLista(lista);
    }

    private void ValidarLista(List<Produto> lista)
    {
        if (lista != null && lista.Count > 0)
            listaProdutos.ItemsSource = lista;
        else
        {
            listaProdutos.IsVisible = false;
            lblListaVazia.IsVisible = true;
        }
    }

    private void ButtonAdicionarProduto_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroProduto());
    }

    private void listaProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        var produto = (Produto)e.SelectedItem;
        if (produto != null)
            NavegarTelaCadastro(produto);

        ((ListView)sender).SelectedItem = null;
    }

    private void NavegarTelaCadastro(Produto produto)
    {
        Navigation.PushAsync(new CadastroProduto(produto));
    }
}