namespace Filantroplanta.Views.Componentizacao;

public partial class BotaoCancelar : ContentView
{
	public BotaoCancelar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextoProperty = BindableProperty.Create(nameof(Texto), typeof(string), typeof(BotaoCancelar));
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