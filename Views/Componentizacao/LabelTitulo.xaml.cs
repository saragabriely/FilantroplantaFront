namespace Filantroplanta.Views.Componentizacao;

public partial class LabelTitulo : ContentView
{
	public LabelTitulo()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TituloProperty = BindableProperty.Create(nameof(Titulo), typeof(string), typeof(LabelTitulo));
    public string Titulo
    {
        get
        {
            return GetValue(TituloProperty) as string;
        }
        set
        {
            SetValue(TituloProperty, value);
        }
    }
}