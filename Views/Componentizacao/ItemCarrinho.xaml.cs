namespace Filantroplanta.Views.Componentizacao;

public partial class ItemCarrinho : ContentView
{
	public ItemCarrinho()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty NomeProdutoProperty = BindableProperty.Create(nameof(NomeProduto), typeof(string), typeof(ItemCarrinho));
    public string NomeProduto
    {
        get
        {
            return GetValue(NomeProdutoProperty) as string;
        }
        set
        {
            SetValue(NomeProdutoProperty, value);
        }
    }

    public static readonly BindableProperty QuantidadeProperty = BindableProperty.Create(nameof(Quantidade), typeof(string), typeof(ItemCarrinho));
    public string Quantidade
    {
        get
        {
            return GetValue(QuantidadeProperty) as string;
        }
        set
        {
            SetValue(QuantidadeProperty, value);
        }
    }

    public static readonly BindableProperty LocalizacaoProperty = BindableProperty.Create(nameof(Localizacao), typeof(string), typeof(ItemCarrinho));
    public string Localizacao
    {
        get
        {
            return GetValue(LocalizacaoProperty) as string;
        }
        set
        {
            SetValue(LocalizacaoProperty, value);
        }
    }

    public static readonly BindableProperty ValorTotalProperty = BindableProperty.Create(nameof(ValorTotal), typeof(string), typeof(ItemCarrinho));
    public string ValorTotal
    {
        get
        {
            return GetValue(ValorTotalProperty) as string;
        }
        set
        {
            SetValue(ValorTotalProperty, value);
        }
    }
}