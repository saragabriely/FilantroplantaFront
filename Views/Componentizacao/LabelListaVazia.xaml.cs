namespace Filantroplanta.Views.Componentizacao;

public partial class LabelListaVazia : ContentView
{
	public LabelListaVazia()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextLabelProperty = BindableProperty.Create(nameof(TextLabel), typeof(string), typeof(LabelListaVazia));
    public string TextLabel
    {
        get
        {
            return GetValue(TextLabelProperty) as string;
        }
        set
        {
            SetValue(TextLabelProperty, value);
        }
    }

    public static readonly BindableProperty TextEntryProperty = BindableProperty.Create(nameof(TextEntry), typeof(string), typeof(LabelListaVazia));
    public string TextEntry
    {
        get
        {
            return GetValue(TextEntryProperty) as string;
        }
        set
        {
            SetValue(TextEntryProperty, value);
        }
    }
}