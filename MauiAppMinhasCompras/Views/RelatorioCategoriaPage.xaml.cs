using MauiAppMinhasCompras.Helpers;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class RelatorioCategoriaPage : ContentPage
{
	public ObservableCollection<RelatorioCategoria> ListaRelatorio {  get; set; }
	public RelatorioCategoriaPage(IEnumerable<RelatorioCategoria> dados)
	{
		InitializeComponent();

		ListaRelatorio = new ObservableCollection<RelatorioCategoria>(dados);
		lst_relatorio.ItemsSource = ListaRelatorio;
	}
}