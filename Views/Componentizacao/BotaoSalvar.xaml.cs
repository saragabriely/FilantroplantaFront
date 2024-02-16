namespace Filantroplanta.Views.Componentizacao;

public partial class BotaoSalvar : ContentView
{
	public BotaoSalvar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextoProperty = BindableProperty.Create(nameof(Texto), typeof(string), typeof(BotaoSalvar));
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