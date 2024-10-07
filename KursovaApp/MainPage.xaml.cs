using System.Collections.ObjectModel;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class MainPage : ContentPage
{
	private readonly University university = new University();

	public ObservableCollection<University> Universities {get; set;}

	public MainPage()
	{
		InitializeComponent();
		
	}

	private void OnButtonClicked(object sender, EventArgs e)
	{
		var list = university.ReadFile();

		DisplayAlert("Alert", list.Count().ToString(), "OK");
	}
}

