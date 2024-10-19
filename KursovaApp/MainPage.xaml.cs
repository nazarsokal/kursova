using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class MainPage : ContentPage
{
	private readonly University university = new University();

	public ObservableCollection<University> Universities {get; set;}

	public List<University> UniversitiesList {get; private set;} 

	public ICommand ButtonCommand { get; }

	public MainPage()
	{
		InitializeComponent();
		Debug.WriteLine("MainPage initialized");

		Universities = new ObservableCollection<University>();

		BindingContext = this;

		ButtonCommand = new Command<University>(AddInfoButton_Clicked);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		UniversitiesList = UniversityRepository.ReadFile();

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

    private void SortButton_Clicked(object sender, EventArgs e)
    {
        string propName = "";
        var buttonPressed = sender as Button;

        if (buttonPressed == NameSortButton)
            propName = "Name";
        else if (buttonPressed == CitySortButton)
            propName = "City";
        else if (buttonPressed == CountrySortButton)
            propName = "Country";
        else if (buttonPressed == StudentsCountSortButton)
            propName = "StudentsCount";
        else if (buttonPressed == PriceSortButton)
            propName = "Price";

        if (buttonPressed.Text == "▲")
        {
            var predDESCsort = UniversityRepository.predDESC;
            var sortedUniversities = new ObservableCollection<University>(UniversityRepository.SortUniversities(predDESCsort, propName));

            buttonPressed.Text = "▼";
            UniversitiesTable.ItemsSource = sortedUniversities;
        }
        else if (buttonPressed.Text == "▼")
        {
            var predASCsort = UniversityRepository.predASC;
            var sortedUniversities = new ObservableCollection<University>(UniversityRepository.SortUniversities(predASCsort, propName));

            buttonPressed.Text = "▲";
            UniversitiesTable.ItemsSource = sortedUniversities;
        }
    }

    private void AddInfoButton_Clicked(University university)
    {
		var additionalInfoPage = new AdditionalInfoPage();
		var aiWindow = new Window(additionalInfoPage);

		App.Current?.OpenWindow(aiWindow);
    }

	private void SpecialSearchButton_Clicked(object sender, EventArgs e)
	{
		// var secondWindow = new SpecialSearchPage();
		// var spWindow = new Window(secondWindow);

		// Application.Current?.OpenWindow(spWindow);
	}
}

