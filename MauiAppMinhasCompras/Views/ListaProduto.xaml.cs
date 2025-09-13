using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>(); 

	public ListaProduto()
	{
		InitializeComponent();

		lst_produto.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
		try
		{
			lista.Clear();

			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());

		} catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		try
		{
			string q = e.NewTextValue;

			lst_produto.IsRefreshing = true;

			lista.Clear();

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		
    }

	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		try
		{
			double soma = lista.Sum(i => i.Total);

			string msg = $"O total é {soma:C}";

			DisplayAlert("Total dos Produtos", msg, "OK");
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

			if(confirm)
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p);
			}
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}

    }

    private void lst_produto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }

    private async void lst_produto_Refreshing(object sender, EventArgs e) //Recarregamento manual da lista
    {
		try
		{
			lista.Clear();

			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		finally 
		{
			lst_produto.IsRefreshing = false;
		}
    }

    private async void txt_search_cat_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string categoria = e.NewTextValue;

            lst_produto.IsRefreshing = true;

            lista.Clear();

            List<Produto> tmp = await App.Db.SearchByCategoria(categoria);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
    {
        try
        {
			var grupos = lista.GroupBy(i => i.Categoria).Select(g => new RelatorioCategoria
			{
				Categoria = g.Key,
				Total = g.Sum(p => p.Total)
			}).ToList();

			await Navigation.PushAsync(new Views.RelatorioCategoriaPage(grupos));	
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}