using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class MainPage : ContentPage
{
	public ObservableCollection<University> Universities {get; set;}
	public List<University> UniversitiesList {get; private set;} 
    public List<University> CurrentState {get; private set; }
	public ICommand ButtonCommand { get; }
    public User _User { get; set; }

	public MainPage()
	{
		InitializeComponent();
		Debug.WriteLine("MainPage initialized");

		Universities = new ObservableCollection<University>();

		UniversitiesList = FileRepository.ReadFile();

		UniversityRepository universityRepository = new UniversityRepository(UniversitiesList);

		foreach (var item in UniversitiesList)
		{
			Universities.Add(new University
			{
				Name = item.Name,
				City = item.City,
				Country = item.Country,
				StudentsCount = item.StudentsCount,
				Price = item.Price,
				Description = item.Description,
				StudyFields = item.StudyFields,
				PhotoPath = item.PhotoPath
			});
		}

		BindingContext = this;
        CurrentState = UniversitiesList;

		ButtonCommand = new Command<University>(AddInfoButton_Clicked);
	}

    private void StudentsCountButtonClicked(object sender, EventArgs e)
    {
		if (StartEntry.Text != null && MaxEntry.Text != null)
        {
            int minStudentCount = int.Parse(StartEntry.Text);
            int maxStudentCount = int.Parse(MaxEntry.Text);
    
            if (minStudentCount != 0 && maxStudentCount != 0)
            {
                CurrentState = UniversityRepository.StudentsCountUniversities(minStudentCount, maxStudentCount);
                var specialSCUniversities = new ObservableCollection<University>(CurrentState);
                if (specialSCUniversities.Count == 0)
                    DisplayAlert("Помилка", "Університетів із заданою кількістю студентів не знайдено", "ОК");
    
                UniversitiesTable.ItemsSource = specialSCUniversities;
            }
            else
                DisplayAlert("Помилка", "Ви не ввели усіх необхідних даних", "ОК");
        }
        else
            UniversitiesTable.ItemsSource = Universities;
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
            var sortedUniversities = new ObservableCollection<University>(UniversityRepository.SortUniversities(predDESCsort, propName, CurrentState));

            buttonPressed.Text = "▼";
            UniversitiesTable.ItemsSource = sortedUniversities;
        }
        else if (buttonPressed.Text == "▼")
        {
            var predASCsort = UniversityRepository.predASC;
            var sortedUniversities = new ObservableCollection<University>(UniversityRepository.SortUniversities(predASCsort, propName, CurrentState));

            buttonPressed.Text = "▲";
            UniversitiesTable.ItemsSource = sortedUniversities;
        }
    }

    private async void AddInfoButton_Clicked(University university) => await Navigation.PushAsync(new AdditionalInfoPage(university, (User)_User));

	private void SpecialSearchButton_Clicked(object sender, EventArgs e)
	{
		var secondWindow = new SpecialSearchPage(UniversitiesList);
		var spWindow = new Window(secondWindow);

		Application.Current?.OpenWindow(spWindow);
	}
}

