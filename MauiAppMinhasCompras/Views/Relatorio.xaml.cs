using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras.Views;

public partial class Relatorio : ContentPage
{
    private List<Produto> Produtos;
    private SQLiteDatabaseHelper dbHelper;

    public Relatorio()
    {
        InitializeComponent();

        // Inicializa o helper com o caminho do banco
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "MinhasCompras.db3");
        dbHelper = new SQLiteDatabaseHelper(dbPath);

        // Carrega os produtos do banco
        CarregarProdutos();
    }

    private async void CarregarProdutos()
    {
        try
        {
            Produtos = await dbHelper.GetAll();
            CollectionViewProdutos.ItemsSource = Produtos;

            // Atualiza o total de todos os produtos
            double totalGasto = Produtos.Sum(p => p.Quantidade * p.Preco);
            lblTotalGasto.Text = $"Total Gasto: R$ {totalGasto:F2}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void OnFiltrarClicked(object sender, EventArgs e)
    {
        try
        {
            var inicio = DatePickerInicio.Date;
            var fim = DatePickerFim.Date.AddDays(1).AddTicks(-1); // inclui todo o dia final

            // Filtra produtos pelo período
            var produtosFiltrados = Produtos
                .Where(p => p.DataCadastro >= inicio && p.DataCadastro <= fim)
                .ToList();

            CollectionViewProdutos.ItemsSource = produtosFiltrados;

            // Atualiza o total gasto no período
            double totalGasto = produtosFiltrados.Sum(p => p.Quantidade * p.Preco);
            lblTotalGasto.Text = $"Total Gasto: R$ {totalGasto:F2}";
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}
