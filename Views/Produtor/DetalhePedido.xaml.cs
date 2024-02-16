using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Models;
using Filantroplanta.Models.API.Pedido;
using Filantroplanta.Views.Componentizacao;

namespace Filantroplanta.Views.Produtor;

public partial class DetalhePedido : ContentPage
{
    public BotaoCancelar btnRecusar = new BotaoCancelar();
    public BotaoSalvar   btnAceitar = new BotaoSalvar();
    public ControleComponentizacao ctrlComponente = new ControleComponentizacao();
    public ControlePedido ctrlPedido = new ControlePedido();
    public ControleUtil   ctrlUtil   = new ControleUtil();
    public ControlePessoa ctrlPessoa = new ControlePessoa();

    public static PedidoAPI pedidoAPI;

    public const string RecusarPedido  = "Recusar";
    public const string AceitarPedido  = "Aceitar";
    public const string EnviarPedido   = "Enviar";
    public const string CancelarPedido = "Cancelar";

    public DetalhePedido()
	{
		InitializeComponent();
    }

    public DetalhePedido(PedidoAPI pedidoProduto)
    {
        InitializeComponent();

        pedidoAPI = pedidoProduto;
        this.PopularTela();
    }

    private void BotoesRecusarAceitar(string botao01, string botao02)
    {
        hsBotoesRecusarAceitar.IsVisible = true;
        hsBotoesRecusarAceitar.Children.Add(btnRecusar);
        hsBotoesRecusarAceitar.Children.Add(btnAceitar);

        var botaoAceitar = btnAceitar.FindByName<Button>(ctrlComponente.NomeBotaoSalvar);
        if (botaoAceitar != null)
        {
            btnAceitar.Texto = botao01;

            if (botao01.Equals(AceitarPedido))
                botaoAceitar.Clicked += this.ButtonAceitar_Clicked;
            else
                botaoAceitar.Clicked += this.ButtonEnviarPedido_Clicked;
        }

        var botaoRecusar = btnRecusar.FindByName<Button>(ctrlComponente.NomeBotaoCancelar);
        if (botaoRecusar != null)
        {
            botaoRecusar.Clicked += this.ButtonRecusar_Clicked;
            btnRecusar.Texto      = botao02;
        }
    }

    private void PopularTela()
    {
        var cliente = ctrlPessoa.BuscarPessoa(pedidoAPI.Pedido.ClienteID);

        if(cliente != null && cliente.PessoaID > 0)
        {
            lblNomeCliente.Text        = cliente.Nome;
            lblTelefoneCliente.Text    = cliente.Telefone;
            lblLocalizacaoCliente.Text = cliente.Cidade;
        }

        if(pedidoAPI.Pedido.StatusPedidoID == StatusPedido.Pendente_Aprovacao)
            this.BotoesRecusarAceitar(AceitarPedido, RecusarPedido);
        else if (pedidoAPI.Pedido.StatusPedidoID == StatusPedido.Pendente_Envio)
            this.BotoesRecusarAceitar(EnviarPedido, CancelarPedido);
        else
            hsBotoesRecusarAceitar.IsVisible = false;

        lblNumeroPedido.Text = pedidoAPI.Pedido.PedidoID.ToString();
        lblStatusPedido.Text = ctrlPedido.BuscarStatusPedido(pedidoAPI.Pedido.StatusPedidoID);

        lblValorTotal.Text = ctrlUtil.FormatarDecimal(pedidoAPI.Pedido.ValorTotalPedido);
        lblDataPedido.Text = pedidoAPI.Pedido.DataCadastro.ToString("dd/MM/yyyy");

        listaProdutos.ItemsSource = pedidoAPI.Produtos;
    }

    private async void ButtonAceitar_Clicked(object sender, EventArgs e) => AtualizarPedido(StatusPedido.Pendente_Envio);
    private async void ButtonEnviarPedido_Clicked(object sender, EventArgs e) => AtualizarPedido(StatusPedido.Finalizado);

    private async void AtualizarPedido(long atualizacao)
    {
        bool resposta = await DisplayAlert("Atualização", "Confirma a atualização do pedido?", "Sim", "Não");

        if (resposta)
        {
            var produtor = ctrlPessoa.BuscarUsuarioLogado();
            pedidoAPI.Pedido.StatusPedidoID = atualizacao;

            if (ctrlPedido.AtualizarPedido(pedidoAPI.Pedido, HttpMethod.Put, produtor.PessoaID, produtor.TipoPessoaID))
            {
                await DisplayAlert("Atualização", "Pedido atualizado com sucesso!", "OK");
                Voltar();
            }
            else
                await DisplayAlert("Atualização de pedido", "Erro ao atualizar pedido", "OK");
        }
    }

    private async void ButtonRecusar_Clicked(object sender, EventArgs e) => AtualizarPedido(StatusPedido.Cancelado_Produtor);

    public async void Voltar() => await Navigation.PopAsync();
}