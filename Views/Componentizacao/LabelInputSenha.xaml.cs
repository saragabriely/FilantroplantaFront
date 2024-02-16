namespace Filantroplanta.Views.Componentizacao;

public partial class LabelInputSenha : ContentView
{
	public LabelInputSenha()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextoLabelProperty = BindableProperty.Create(nameof(TextoLabel), typeof(string), typeof(LabelInput));
    public string TextoLabel
    {
        get
        {
            return GetValue(TextoLabelProperty) as string;
        }
        set
        {
            SetValue(TextoLabelProperty, value);
        }
    }

    public static readonly BindableProperty TextoEntryProperty = BindableProperty.Create(nameof(TextoEntry), typeof(string), typeof(LabelInput));
    public string TextoEntry
    {
        get
        {
            return GetValue(TextoEntryProperty) as string;
        }
        set
        {
            SetValue(TextoEntryProperty, value);
        }
    }
}