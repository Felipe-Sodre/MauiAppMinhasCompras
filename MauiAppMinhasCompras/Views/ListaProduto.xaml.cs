namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    public ListaProduto()
    {
        InitializeComponent();
    }

	//Conec��o com o bot�o adicionar
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		//Caso haja erro
		try
		{		//Linha de c�digo que abre outra pagina
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}

    }

}