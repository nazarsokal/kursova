using System.Collections.ObjectModel;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class MainPage : ContentPage
{
	private readonly University university = new University();

	public ObservableCollection<University> Universities {get; set;}

	public List<University> UniversitiesFromFile {get; private set;} 

	public MainPage()
	{
		InitializeComponent();

		Universities = new ObservableCollection<University>();

		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		UniversitiesFromFile = university.ReadFile();
		
		foreach (var item in UniversitiesFromFile)
		{
			Universities.Add(new University
			{
				Name = item.Name,
				City = item.City,
				Country = item.Country,
				StudentsCount = item.StudentsCount,
				Price = item.Price
			});
		}
	}

	private void OnButtonClicked(object sender, EventArgs e)
	{
		//DisplayAlert("Alert", list.Count().ToString(), "OK");
	}

	private void TableInput()
	{
		UniversitiesFromFile = university.ReadFile();
		foreach (var item in UniversitiesFromFile)
		{
			Universities.Add(new University
			{
				Name = item.Name,
				City = item.City,
				Country = item.Country,
				StudentsCount = item.StudentsCount,
				Price = item.Price
			});
		}
	}
}

