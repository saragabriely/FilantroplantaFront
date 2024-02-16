using Filantroplanta.API;
using Filantroplanta.Controle;
using Filantroplanta.Controle.Pessoa;
using Filantroplanta.Models.API;
using Filantroplanta.Views.Componentizacao;

namespace Filantroplanta.Views.Usuario;

public partial class AlterarSenha : ContentPage
{
    public BotaoCancelar btnCancelar = new();
    public BotaoSalvar btnSalvar = new();
    public ControleComponentizacao controleComponente = new();
    readonly ControlePessoa controlePessoa = new();
    readonly PessoaController controller = new();

    public AlterarSenha()
    {
        InitializeComponent();

        this.BotoesCancelarSalvar();
    }

    private void BotoesCancelarSalvar()
    {
        hsBotoesSalvarCancelar.Children.Add(btnCancelar);
        hsBotoesSalvarCancelar.Children.Add(btnSalvar);

        var botaoSalvar = btnSalvar.FindByName<Button>(controleComponente.NomeBotaoSalvar);
        if (botaoSalvar != null)
        {
            botaoSalvar.Clicked += this.ButtonSalvar_Clicked;
            btnSalvar.Texto = "Salvar";
        }

        var botaoCancelar = btnCancelar.FindByName<Button>(controleComponente.NomeBotaoCancelar);
        if (botaoCancelar != null)
        {
            botaoCancelar.Clicked += this.ButtonCancelar_Clicked;
            btnCancelar.Texto = "Cancelar";
        }
    }

    public void Voltar()
    {
        Navigation.PopAsync();
    }

    private void ButtonCancelar_Clicked(object sender, EventArgs e)
    {
        Voltar();
    }

    private void ButtonSalvar_Clicked(object sender, EventArgs e)
    {
        ValidarSenha();
    }

    private async void ValidarSenha()
    {
        var senha = entSenha.TextoEntry;
        var confirmarSenha = entConfirmaSenha.TextoEntry;

        if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confirmarSenha))
        {
            await DisplayAlert("Campo vazio", "Preencha todos os campos para confirmar a alteração da senha.", "Ok");
        }
        else if (!senha.Equals(confirmarSenha))
        {
            await DisplayAlert("Campos diferentes", "As senhas não coincidem.", "Ok");
        }
        else
        {
            AlteraSenha(senha);

            await DisplayAlert("Alteração de Senha", "Senha alterada com sucesso!", "Ok");

            Voltar();
        }
    }

    private async void AlteraSenha(string novaSenha)
    {
        var pessoa = controlePessoa.BuscarUsuarioLogado();
        var retorno = new Models.API.Response.ResponseBase();

        if (pessoa != null)
        {
            pessoa.Senha = novaSenha;

            retorno = controller.CadastrarPessoa(pessoa);

            if (retorno != null)
            {
                if (retorno.StatusRetorno == 200)
                {
                    if (pessoa.PessoaID > 0)
                    {
                        controlePessoa.AdicionarSalvarPessoaCache(pessoa, $"Pessoa_{pessoa.PessoaID}");
                    }
                }
                else
                    await DisplayAlert("Falha ao realizar alteração de senha.", $"Erro: {retorno.Erro}", "OK");
            }
        }
    }
}