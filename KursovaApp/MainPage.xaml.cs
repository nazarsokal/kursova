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
		int minStudentCount = int.Parse(StartEntry.Text);
		int maxStudentCount = int.Parse(MaxEntry.Text);

        if (minStudentCount != 0 && maxStudentCount != 0)
        {
            var specialSCUniversities = new ObservableCollection<University>(UniversityRepository.StudentsCountUniversities(minStudentCount, maxStudentCount));
            if (specialSCUniversities.Count == 0)
                DisplayAlert("Помилка", "Університетів із заданою кількістю студентів не знайдено", "ОК");

            UniversitiesTable.ItemsSource = specialSCUniversities;
        }
        else
            DisplayAlert("Помилка", "Ви не ввели усіх необхідних даних", "ОК");
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
		var filterText = ((SearchBar)sender).Text;
		var universities = new ObservableCollection<University>(UniversityRepository.SearchUnivesities(filterText));

		UniversitiesTable.ItemsSource = universities;
    }
}

