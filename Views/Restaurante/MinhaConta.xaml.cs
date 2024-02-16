using Filantroplanta.Controle.Pessoa;

namespace Filantroplanta.Views.Restaurante;

public partial class MinhaConta : ContentPage
{
    public ControlePessoa controlePessoa = new ControlePessoa();
    public Models.Pessoa usuarioLogado = new Models.Pessoa();

    public MinhaConta()
    {
        InitializeComponent();

        this.usuarioLogado = controlePessoa.BuscarUsuarioLogado();

        if (this.usuarioLogado != null)
            lblMinhaConta.Titulo = $"Olá, {this.usuarioLogado.Nome}";
    }

    private void btnMeusDados_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Usuario.CadastroPessoa(this.usuarioLogado));
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
            controlePessoa.Logout();
            App.Current.MainPage = new Login();
        }
    }
}