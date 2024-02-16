using Filantroplanta.Controle;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Models;
using Filantroplanta.Views.Componentizacao;
using System.Net;

namespace Filantroplanta.Views.Produtor;

public partial class Estoque : ContentPage
{
    public Produto ProdutoSelecionado { get; set; }
    public v_Estoque     v_Estoque    { get; set; }
    public BotaoCancelar btnCancelar = new BotaoCancelar();
    public BotaoSalvar   btnSalvar   = new BotaoSalvar();
    public ControleComponentizacao controleComponente = new ControleComponentizacao();

    public Estoque()
    {
        InitializeComponent();
    }

    public Estoque(v_Estoque v_Estoque)
	{
		InitializeComponent();

        this.v_Estoque = v_Estoque;
        BotoesCancelarSalvar();
        PopularCampos(v_Estoque);
    }

    private void BotoesCancelarSalvar()
    {
        hsBotoesSalvarCancelar.Children.Add(btnCancelar);
        hsBotoesSalvarCancelar.Children.Add(btnSalvar);

        var botaoSalvar = btnSalvar.FindByName<Button>(controleComponente.NomeBotaoSalvar);
        if (botaoSalvar != null)
        {
            botaoSalvar.Clicked += this.ButtonSalvar_Clicked;
            botaoSalvar.Text = "Salvar";
        }

        var botaoCancelar = btnCancelar.FindByName<Button>(controleComponente.NomeBotaoCancelar);
        if (botaoCancelar != null)
        {
            botaoCancelar.Clicked += this.ButtonCancelar_Clicked;
            botaoCancelar.Text = "Cancelar";
        }
    }

    private void ButtonCancelar_Clicked(object sender, EventArgs e) => Voltar();

    private void Voltar() => Navigation.PopAsync();

    private async void ButtonSalvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var qtdeDisponivel = entQtdeDisponivel.Text.Trim();

            if (!string.IsNullOrEmpty(qtdeDisponivel))
            {
                var qtdeNumerica = Convert.ToInt32(qtdeDisponivel);

                if (qtdeNumerica > 0)
                {
                    if (qtdeNumerica != this.v_Estoque.Estoque.QuantidadeDisponivel)
                    {
                        var controleEstoque = new Controle.Produtor.ControleEstoque();

                        this.v_Estoque.Estoque.QuantidadeDisponivel = qtdeNumerica;

                        var retorno = controleEstoque.AtualizarEstoque(this.v_Estoque.Estoque);

                        if (retorno.StatusRetorno == (int)HttpStatusCode.OK)
                        {
                            await DisplayAlert("Atualização de Estoque", "Estoque atualizado com sucesso!", "OK");
                            Voltar();
                        }
                        else
                            await DisplayAlert("Erro", "Erro na atualização do estoque", "OK");
                    }
                }
            }
            else
                LancarExcecaoCampoVazio("QUANTIDADE");
        }
        catch(Exception ex)
        {
            await DisplayAlert("Erro - Estoque", ex.Message, "OK");
        }
    }

    public void PopularCampos(v_Estoque v_Estoque)
    {
        if(v_Estoque != null)
        {
            lblProduto.Text        = v_Estoque.NomeProduto;
            entQtdeDisponivel.Text = v_Estoque.Estoque.QuantidadeDisponivel.ToString();
            entQtdeReservada.Text  = v_Estoque.Estoque.QuantidadeReservada.ToString();
            entQtdeVendida.Text    = v_Estoque.Estoque.QuantidadeVendida.ToString();

            this.ProdutoSelecionado = v_Estoque.Produto;
        }  
    }

    public async void LancarExcecaoCampoVazio(string campo)
    {
        await DisplayAlert("Campo Vazio", $"Popule o campo '{campo}'", "OK");
    }
}