using System.Net;
using System.Windows.Input;
using Filantroplanta.Controle;
using Filantroplanta.Views.Usuario;

namespace Filantroplanta.Views;

public partial class Login : ContentPage
{
    public ControleLogin controleLogin = new ControleLogin();

    public Login()
	{
		InitializeComponent();
	}

    public void RealizarCadastro_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroPessoa());
    }

    public void btnLogin_Clicked(object sender, EventArgs e)
    {
        ValidarLogin();
    }

    private async void ValidarLogin()
    {
        var retorno = ValidarUsuario();

        if(retorno.Equals("Produtor"))
            App.Current.MainPage = new Produtor.PaginaInicial();
        else if(retorno.Equals("Restaurante"))
            App.Current.MainPage = new Restaurante.PaginaInicial();
        else
            await DisplayAlert("Falha ao realizar login", retorno, "OK");
    }

    private string ValidarUsuario()
    {
        var login = entUsuario.Text;
        var senha = entSenha.Text;

        if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
        {
            return controleLogin.ExecutarLogin(login, senha);
        }
        else
            return "Preencha Usuário e Senha";
    }
}