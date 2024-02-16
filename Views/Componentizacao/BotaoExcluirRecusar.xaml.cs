namespace Filantroplanta.Views.Componentizacao;

public partial class BotaoExcluirRecusar : ContentView
{
	public BotaoExcluirRecusar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextoProperty = BindableProperty.Create(nameof(Texto), typeof(string), typeof(BotaoExcluirRecusar));
    public string Texto
    {
        get
        {
            return GetValue(TextoProperty) as string;
        }
        set
        {
            SetValue(TextoProperty, value);
        }
    }
}