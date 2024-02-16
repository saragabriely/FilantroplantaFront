using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Restaurante;
using Filantroplanta.Models;
using System.Globalization;

namespace Filantroplanta.Views.Restaurante;

public partial class Carrinho : ContentPage
{
    public ControleCarrinho ctrlCarrinho = new ControleCarrinho();
    public ControlePessoa   ctrlPessoa   = new ControlePessoa();
    public ControleUtil     controleUtil = new ControleUtil();
    public Pessoa           restaurante  = new Pessoa();
    const int RefreshDuration = 1;
    public List<v_ItemCarrinho> Items { get; set; }

    public Carrinho()
    {
        InitializeComponent();

        restaurante = ctrlPessoa.BuscarUsuarioLogado();

        if (restaurante != null && restaurante.PessoaID > 0)
            BuscarItensCarrinho(restaurante.PessoaID);

        BindingContext = this;

        listaItensCarrinho.Loaded += (sender, args) =>
        {
            listaItensCarrinho.SelectionMode = ListViewSelectionMode.Single;
        };

        listaItensCarrinho.IsRefreshing = false;
    }

    public void LoadContent()
    {
        AtualizarCarrinhoAsync();
    }

    private void AtualizarCarrinhoAsync()
    {
        if (restaurante != null && restaurante.PessoaID > 0)
        {
            var lista = ctrlCarrinho.BuscarListaCarrinhoCache(restaurante.PessoaID);

            if (lista != null && lista.Count > 0)
                PopularTelaCarrinho(lista);
            else
                EsconderLista();
        }
    }

    private void BuscarItensCarrinho(long idRestaurante)
    {
        var listaItens = ctrlCarrinho.BuscarListaCarrinhoCache(idRestaurante);

        PopularTelaCarrinho(listaItens);
    }

    private void PopularTelaCarrinho(List<v_ItemCarrinho> listaItens)
    {
        listaItensCarrinho = new ListView();

        if (listaItens != null && listaItens.Count > 0)
        {
            var soma = listaItens.Sum(i => i.ValorTotal);

            lblValorTotal.Text = controleUtil.FormatarDecimal(soma);

            MostrarAtualizarListaTela(listaItens);
        }
        else
            EsconderLista();
    }

    public void MostrarAtualizarListaTela(List<v_ItemCarrinho> listaItens)
    {
        Items = listaItens;
        listaItensCarrinho.ItemsSource = listaItens;
        listaItensCarrinho.IsVisible  = true;
        scrollItensCarrinho.IsVisible = true;
        lblListaVazia.IsVisible       = false;
        hstValorTotal.IsVisible       = true;
        slBtnFinalizar.IsVisible      = true;
    }

    public void EsconderLista()
    {
        scrollItensCarrinho.IsVisible = false;
        listaItensCarrinho.IsVisible  = false;
        lblListaVazia.IsVisible       = true;
        hstValorTotal.IsVisible       = false;
        slBtnFinalizar.IsVisible      = false;
    }

    private async void ButtonAtualizar_Clicked(object sender, EventArgs e) => AtualizarCarrinhoAsync();

    private void ButtonFinalizar_Clicked(object sender, EventArgs e) => Navigation.PushAsync(new FinalizarCarrinho(Items, restaurante));

    private void ListaItensCarrinho_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        ((ListView)sender).SelectedItem = null;
    }

    private async void ButtonExcluirItem_Pressed(object sender, EventArgs e)
    {
        bool resposta = await DisplayAlert("Exclusão", "Confirma a exclusão do produto?", "Sim", "Não");

        if (resposta)
        {
            if (sender is ImageButton button)
            {
                if (button.CommandParameter is v_ItemCarrinho item)
                    ExcluirItemCarrinho(item);
            }
        }

        await Navigation.PushAsync(new Carrinho());
    }

    private async void ExcluirItemCarrinho(v_ItemCarrinho item)
    {
        var retorno = ctrlCarrinho.ExcluirItemCarrinho(item, restaurante.PessoaID);

        if (retorno)
        {
            var listaItemCarrinho = ctrlCarrinho.BuscarListaItemCarrinhoCache(restaurante.PessoaID);

            if (listaItemCarrinho != null && listaItemCarrinho.Count > 0)
                MostrarAtualizarListaTela(listaItemCarrinho);
            else
                EsconderLista();

            await DisplayAlert("Carrinho", "Item excluído com sucesso", "OK");
        }
        else
            await DisplayAlert("Carrinho", "Não foi possível excluir o item selecionado.", "OK");
    }
}
