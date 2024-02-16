using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Mock;
using Filantroplanta.Models;

namespace Filantroplanta.Views.Produtor;

public partial class MinhaConta : ContentPage
{
    public ControlePessoa ctrlPessoa = new ControlePessoa();

	public MinhaConta()
	{
		InitializeComponent();

        var pessoa = ctrlPessoa.BuscarUsuarioLogado();

        if (pessoa != null)
            lblMinhaConta.Titulo = $"Olá, {pessoa.Nome}";
    }

    private void btnMeusDados_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Usuario.CadastroPessoa(ctrlPessoa.BuscarUsuarioLogado()));
    }

    private void btnAlterarSenha_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Usuario.AlterarSenha());
    }

    private async void btnSair_Clicked(object sender, EventArgs e)
    {
        bool resposta = await DisplayAlert("Saindo", "Realmente deseja sair?", "Sim", "Não");

        if (resposta)
        {
            ctrlPessoa.Logout();
            App.Current.MainPage = new Login();
        }
    }
}