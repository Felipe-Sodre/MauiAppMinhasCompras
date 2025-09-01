using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    public ListaProduto()
    {
        InitializeComponent();

		lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();

        tmp.ForEach(i => lista.Add(i));
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

    private async void text_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total �{soma:C}";

        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        //
    }
}