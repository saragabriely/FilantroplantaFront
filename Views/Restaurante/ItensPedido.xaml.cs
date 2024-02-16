using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Restaurante;
using Filantroplanta.Models;
using Filantroplanta.Models.API;
using Filantroplanta.Models.API.Pedido;
using Filantroplanta.Views.Componentizacao;
using System.Globalization;

namespace Filantroplanta.Views.Restaurante;

public partial class ItensPedido : ContentPage
{
    public BotaoCancelar btnVoltar_ = new BotaoCancelar();
    public BotaoSalvar btnCancelar = new BotaoSalvar();
    public ControleComponentizacao ctrlComponente = new ControleComponentizacao();
    public ControlePedido controlePedido = new ControlePedido();
    public ControlePessoa controlePessoa = new ControlePessoa();
    public ControleUtil controleUtil = new ControleUtil();
    public Pessoa restaurante = new Pessoa();
    public PedidoAPI pedidoAPI;

    public ItensPedido(long pedidoID)
    {
        InitializeComponent();

        restaurante = controlePessoa.BuscarUsuarioLogado();

        if (restaurante != null && restaurante.PessoaID > 0)
            BuscarItensPedido(restaurante.PessoaID, pedidoID);
    }

    private void BotoesVoltarCancelar()
    {
        slBtnVoltar.IsVisible = false;

        hsBotaoCancelar.IsVisible = true;
        hsBotaoCancelar.Children.Add(btnVoltar_);
        hsBotaoCancelar.Children.Add(btnCancelar);

        var btnCancelarPedido = btnCancelar.FindByName<Button>(ctrlComponente.NomeBotaoSalvar);
        if (btnCancelarPedido != null)
        {
            btnCancelarPedido.Text = "Cancelar";
            btnCancelarPedido.Clicked += this.ButtonCancelar_Clicked;
        }

        var botaoVoltar = btnVoltar_.FindByName<Button>(ctrlComponente.NomeBotaoCancelar);
        if (botaoVoltar != null)
        {
            botaoVoltar.Clicked += this.ButtonVoltar_Clicked;
            botaoVoltar.Text = "Voltar";
        }
    }

    private void ButtonCancelar_Clicked(object sender, EventArgs e) => AtualizarPedido(StatusPedido.Cancelado_Cliente);

    private async void AtualizarPedido(long atualizacao)
    {
        bool resposta = await DisplayAlert("Atualização", "Confirma a atualização do pedido?", "Sim", "Não");

        if (resposta)
        {
            var cliente = controlePessoa.BuscarUsuarioLogado();
            pedidoAPI.Pedido.StatusPedidoID = atualizacao;

            if (controlePedido.AtualizarPedido(pedidoAPI.Pedido, HttpMethod.Put, cliente.PessoaID, cliente.TipoPessoaID))
            {
                await DisplayAlert("Atualização", "Pedido atualizado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Atualização de pedido", "Erro ao atualizar pedido", "OK");
        }
    }

    private void BuscarItensPedido(long idRestaurante, long pedidoID)
    {
        pedidoAPI = new PedidoAPI();
        pedidoAPI = controlePedido.BuscarPedidoAPICache(pedidoID, idRestaurante, Pessoa.Restaurante);

        PopularTelaPedido(pedidoAPI);
    }

    private void PopularTelaPedido(PedidoAPI pedido)
    {
        if (pedido.Produtos != null && pedido.Produtos.Count > 0)
        {
            var soma = pedido.Produtos.Sum(i => i.ValorTotalProduto);

            lblValorTotal.Text   = controleUtil.FormatarDecimal(soma);
            lblDataPedido.Text   = pedido.Pedido.DataCadastro.ToString("dd/MM/yyyy"); 
            lblNumeroPedido.Text = pedido.Pedido.PedidoID.ToString();
            lblStatusPedido.Text = controlePedido.BuscarStatusPedido(pedido.Pedido.StatusPedidoID);

            listaItensPedido.ItemsSource = pedido.Produtos;
            scrollItensPedido.IsVisible = true;

            if (pedido.Pedido.StatusPedidoID == StatusPedido.Pendente_Aprovacao || pedido.Pedido.StatusPedidoID == StatusPedido.Pendente_Envio)
                BotoesVoltarCancelar();
            else
            {
                slBtnVoltar.IsVisible = true;
                hsBotaoCancelar.IsVisible = false;
            }
        }
    }

    private async void ButtonVoltar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}