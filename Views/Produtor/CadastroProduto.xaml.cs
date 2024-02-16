using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Mock;
using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Request;
using Filantroplanta.Views.Componentizacao;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Filantroplanta.Views.Produtor;

public partial class CadastroProduto : ContentPage
{
    public Produto produto { get; set; }
    public Pessoa  produtor { get; set; }

    public ControleProduto ctrlProduto      = new ControleProduto();
    public BotaoCancelar   btnCancelar      = new BotaoCancelar();
    public BotaoSalvar     btnSalvar        = new BotaoSalvar();
    public BotaoExcluirRecusar  btnExcluirRecusar = new BotaoExcluirRecusar();
    public ControleComponentizacao ctrlComponente = new ControleComponentizacao();

    public CadastroProduto()
	{
		InitializeComponent();

        this.BuscarProdutorMemoria();
        this.BotoesCancelarSalvar("Finalizar");
    }

    private void BotoesCancelarSalvar(string descricaoSalvar)
    {
        hsBotoesSalvarCancelar.Children.Add(btnCancelar);
        hsBotoesSalvarCancelar.Children.Add(btnSalvar);

        var botaoSalvar = btnSalvar.FindByName<Button>(ctrlComponente.NomeBotaoSalvar);
        if (botaoSalvar != null)
        {
            botaoSalvar.Clicked += this.ButtonSalvar_Clicked;
            btnSalvar.Texto = descricaoSalvar;
        }

        var botaoCancelar = btnCancelar.FindByName<Button>(ctrlComponente.NomeBotaoCancelar);
        if (botaoCancelar != null)
        {
            botaoCancelar.Clicked += this.ButtonCancelar_Clicked;
            btnCancelar.Texto = "Cancelar";
        }
    }

    private void BotaoExcluir()
    {
        slBotaoExcluir.Children.Add(btnExcluirRecusar);

        var botaoExcluir = btnExcluirRecusar.FindByName<Button>(ctrlComponente.NomeBotaoExcluirRecusar);
        if (botaoExcluir != null)
        {
            botaoExcluir.Clicked  += this.ButtonExcluir_Clicked;
            botaoExcluir.IsVisible = true;
            btnExcluirRecusar.Texto = "Excluir";
        }
    }

    public CadastroProduto(Produto produto)
    {
        InitializeComponent();

        this.BotoesCancelarSalvar("Salvar");
        this.BotaoExcluir();

        entQtde.IsVisible = false;

        if (produto != null)
        {
            this.produto = produto;

            entNomeProduto.TextoEntry = this.produto.Descricao;
            entValorPorKG.TextoEntry  = this.produto.ValorPorKG.ToString();

            slBotaoExcluir.IsVisible   = true;
        }
    }

    private void BuscarProdutorMemoria()
    {
        ControlePessoa controlePessoa = new ControlePessoa();

        if (this.produtor == null || this.produtor.PessoaID == 0)
            this.produtor = controlePessoa.BuscarUsuarioLogado();
    }

    private void ButtonSalvar_Clicked(object sender, EventArgs e) => RealizarCadastroProduto();

    private async void ButtonExcluir_Clicked(object sender, EventArgs e)
    {
        if(this.produto != null)
        {
            bool resposta = await DisplayAlert("Exclusão", "Confirma a exclusão do produto?", "Sim", "Não");

            if(resposta)
                ExcluirProduto(this.produto);
        }
        else
            await DisplayAlert("Produto vazio", "Ocorreu algum problema com o produto", "OK");
    }

    private void ExcluirProduto(Produto produto)
    {
        if(produto != null && produto.ProdutoID > 0)
        {
            var listaProdutos = ctrlProduto.ExcluirProduto(produto);

            Voltar(listaProdutos);
        }
    }

    private void ButtonCancelar_Clicked(object sender, EventArgs e) => Navigation.PopAsync(); // Voltar

    public void Voltar(List<Produto> listaProdutos) => Navigation.PushAsync(new Produtos(listaProdutos));

    private async void RealizarCadastroProduto()
    {
        long pessoaID   = this.produtor != null && this.produtor.PessoaID > 0 ? this.produtor.PessoaID : 0;
        var nomeProduto = entNomeProduto.TextoEntry;
        var quantidade  = entQtde.TextoEntry;
        var valorPorKG  = entValorPorKG.TextoEntry;

        if (string.IsNullOrEmpty(nomeProduto))
            LancarExcecaoCampoVazio("NOME DO PRODUTO");

        else if (string.IsNullOrEmpty(valorPorKG))
            LancarExcecaoCampoVazio("VALOR");

        else
        {
            if(this.produto != null && this.produto.ProdutoID > 0)
            {
                this.produto.Descricao  = nomeProduto;
                this.produto.ValorPorKG = Convert.ToDecimal(valorPorKG);

                ctrlProduto.SalvarAdicionarProduto(null, this.produto, HttpMethod.Put);

                await DisplayAlert("Cadastro atualizado", "Cadastro atualizado com sucesso!", "OK");
            }
            else
            {
                var novo     = new ProdutoRequest();
                novo.Produto = new Produto();

                novo.Produto.Descricao  = nomeProduto;
                novo.Quantidade         = Convert.ToInt64(quantidade);
                novo.Produto.ValorPorKG = Convert.ToDecimal(valorPorKG);
                novo.Produto.ProdutorID = pessoaID;

                ctrlProduto.SalvarAdicionarProduto(novo, null, HttpMethod.Post);

                await DisplayAlert("Cadastro realizado", "Cadastro realizado com sucesso!", "OK");
            }

            Voltar(ctrlProduto.BuscarListaProdutoCache(pessoaID));
        }
    }

    public async void LancarExcecaoCampoVazio(string campo) => await DisplayAlert("Campo Vazio", $"Popule o campo '{campo}'", "OK");
}