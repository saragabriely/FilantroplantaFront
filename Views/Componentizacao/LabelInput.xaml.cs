namespace Filantroplanta.Views.Componentizacao;

public partial class LabelInput : ContentView
{
	public LabelInput()
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

    public static readonly BindableProperty PlaceholderEntryProperty = BindableProperty.Create(nameof(PlaceholderEntry), typeof(string), typeof(LabelInput));
    public string PlaceholderEntry
    {
        get
        {
            return GetValue(PlaceholderEntryProperty) as string;
        }
        set
        {
            SetValue(PlaceholderEntryProperty, value);
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