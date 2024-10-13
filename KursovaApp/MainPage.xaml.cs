using System.Collections.ObjectModel;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class MainPage : ContentPage
{
	private readonly University university = new University();

	public ObservableCollection<University> Universities {get; set;}

	public List<University> UniversitiesList {get; private set;} 

	public MainPage()
	{
		InitializeComponent();

		Universities = new ObservableCollection<University>();

		BindingContext = this;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		UniversitiesList = university.ReadFile();

		UniversityRepository universityRepository = new UniversityRepository(UniversitiesList);

		foreach (var item in UniversitiesList)
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

	private void OnSearchButtonClicked(object sender, EventArgs e)
	{
		// if(SearchEntry.Text != null)
		// {
		// 	Universities.Clear();
		// 	List<University> searchedList = UniversitiesList.Where(n => n.Name.ToLower() == SearchEntry.Text.ToLower()).ToList();
		// 	if(searchedList.Count() == 0)
		// 		DisplayAlert("Помилка", "Університету з такою назвою не знайдено", "ОК");
		// 	else
		// 		TableInput(searchedList);
		// }
	}

	private void TableInput(List<University> universities)
	{
		foreach (var item in universities)
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

    private void StudentsCountButtonClicked(object sender, EventArgs e)
    {
		var minStudentCount = int.Parse(StartEntry.Text);
		var maxStudentCount = int.Parse(MaxEntry.Text);

		if (minStudentCount != null || maxStudentCount != null)
		{
			List<University> specialCountUniversities = UniversitiesList.
				Where(sc => sc.StudentsCount > minStudentCount && sc.StudentsCount < maxStudentCount)
				.ToList();
	
			if(specialCountUniversities.Count() == 0)
				DisplayAlert("Помилка", "Університетів із заданою кількістю студентів не знайдено", "ОК");
			else
				TableInput(specialCountUniversities);
		}
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
		var filterText = ((SearchBar)sender).Text;
		var universities = new ObservableCollection<University>(UniversityRepository.SearchUnivesities(filterText));

		UniversitiesTable.ItemsSource = universities;
    }
}

