namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //Minha pagina inicial = Pasta.Arquivo
            //MainPage = new NavigationPage ( new Views.ListaProduto());
        }
    }
}
