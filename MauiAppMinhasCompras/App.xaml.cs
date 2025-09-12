using MauiAppMinhasCompras.Helpers;
using System.Globalization;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db; //Campo - onde os dados estão gravados

        public static SQLiteDatabaseHelper Db //Propriedade - forma de acesso ao campo
        {
            get
            {
                if(_db == null) //Verifica se o _db é vazio
                {
                    string path = Path.Combine( //Caminho do arquivo .db3
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), 
                        "banco_sqlite_compras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }

                return _db;
            }
        } 
        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
            

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
