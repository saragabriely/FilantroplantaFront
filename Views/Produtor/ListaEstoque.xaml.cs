using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Controle.Produtor;
using Filantroplanta.Models;
using Microsoft.VisualStudio.PlatformUI;
using PropertyChanged;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Filantroplanta.Views.Produtor;

[AddINotifyPropertyChangedInterface]
public partial class ListaEstoque : ContentPage
{
    public ControlePessoa  ctrlPessoa  = new ControlePessoa();
    public ControleEstoque ctrlEstoque = new ControleEstoque();

    const int RefreshDuration = 3;

    public List<v_Estoque> Items { get; set; }

    public ListaEstoque()
	{
		InitializeComponent();

        BuscarEstoquesCache();

        BindingContext = this;

        lvControleEstoque.Loaded += (sender, args) =>
        {
            lvControleEstoque.SelectionMode = ListViewSelectionMode.Single;
        };
    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));

        AtualizarListaEstoqueAsync();

        refreshView.IsRefreshing = false;
    }

    private void AtualizarListaEstoqueAsync()
    {
        var lista = AtualizarListaEstoque();

        if (lista != null)
            AtualizarListaTela(lista);
    }

    private void BuscarEstoquesCache()
    {
        var produtor = ctrlPessoa.BuscarUsuarioLogado();

        AtualizarListaTela(ctrlEstoque.BuscarListaEstoqueCache(produtor.PessoaID));

        lvControleEstoque.IsRefreshing = false;
    }

    private List<Models.Estoque> AtualizarListaEstoque()
    {
        var produtor = ctrlPessoa.BuscarUsuarioLogado();
    
        var lista = ctrlEstoque.BuscarEstoquePorProdutor(produtor.PessoaID);
    
        if (lista != null)
            ctrlEstoque.AtualizarOuCriarListaEstoque(lista, produtor.PessoaID);

        return lista;
    }

    private void AtualizarListaTela(List<Models.Estoque> estoques)
    {
        if (estoques != null && estoques.Count > 0)
        {
            var lista = ctrlEstoque.PopularVEstoque(estoques);

            if (lvControleEstoque.IsRefreshing)
                lvControleEstoque = new ListView();

            Items = lista;
            lvControleEstoque.ItemsSource = lista;
            lvControleEstoque.IsVisible   = true;
            lblListaVazia.IsVisible       = false;
        }
        else
        {
            lvControleEstoque.IsVisible = false;
            lblListaVazia.IsVisible = true;
        }
    }

    private void lvControleEstoque_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        var v_Estoque = (v_Estoque) e.SelectedItem;
        if (v_Estoque != null)
            NavegarTelaEstoque(v_Estoque);

        ((ListView)sender).SelectedItem = null;
    }

    private async void NavegarTelaEstoque(v_Estoque v_Estoque)
    {
        await Navigation.PushAsync(new Estoque(v_Estoque));
    }
}