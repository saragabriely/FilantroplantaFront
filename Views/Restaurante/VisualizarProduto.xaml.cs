using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Restaurante;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Restaurante;

public partial class VisualizarProduto : ContentPage
{
    public ControleUtil     controleUtil     = new ControleUtil();
    public ControleCarrinho controleCarrinho = new ControleCarrinho();
    public ControlePessoa   controlePessoa   = new ControlePessoa();
    public Pessoa           usuarioLogado    = new Pessoa();
    public v_Produto        produto          = new v_Produto();

    public VisualizarProduto()
	{
		InitializeComponent();
	}

    public VisualizarProduto(v_Produto produto)
    {
        InitializeComponent();

        this.usuarioLogado = controlePessoa.BuscarUsuarioLogado();

        if (produto != null)
        {
            this.produto = produto;

            entNomeProduto.Text         = produto.Descricao;
            lblNomeProdutor.Text        = produto.NomeProdutor;
            lblLocalizacaoProdutor.Text = produto.Localizacao;
            lblValorPorKG.Text          = controleUtil.FormatarDecimal(produto.ValorPorKG);
        }
    }

    private async void BotaoAdicionarCarrinho_Clicked(object sender, EventArgs e)
    {
        var quantidade = !string.IsNullOrEmpty(entQtde.Text) ? Convert.ToInt32(entQtde.Text) : 0;

        if (quantidade == 0)
            await DisplayAlert("Quantidade", "Informe a quantidade desejada", "OK");
        else
        {
            if(controleCarrinho.InserirItemCarrinho(this.usuarioLogado, this.produto, quantidade))
            {
                var atualizarCarrinho = new Carrinho();
                atualizarCarrinho.LoadContent();

                await DisplayAlert("Carrinho", "Item adicionado ao carrinho com sucesso!", "OK");

                await Navigation.PopAsync();
            }
        }
    }
}