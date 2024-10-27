using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class MainPage : ContentPage
{
	public ObservableCollection<University> Universities {get; set;}
    public ObservableCollection<string> CountryList { get; set; } = new ObservableCollection<string>();
    public ObservableCollection<string> FilteredCountryList { get; set; } = new ObservableCollection<string>();

	public List<University> UniversitiesList {get; private set;} 
    public List<University> CurrentState {get; private set; }
	public ICommand ButtonCommand { get; }
    public User _User { get; set; }
    private string SelectedCountry;

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

        PopulatePicker();
        CountrySuggestions.ItemsSource = FilteredCountryList;

		ButtonCommand = new Command<University>(AddInfoButton_Clicked);
	}

    private void PopulatePicker()
    {
        var countries = UniversitiesList
                        .Select(u => u.Country)
                        .Distinct()
                        .OrderBy(country => country);

        CountryList.Clear();
        foreach (var country in countries)
        {
            CountryList.Add(country);
        }
        FilteredCountryList = new ObservableCollection<string>(CountryList);
    }
    private void OnCountryEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchTerm = e.NewTextValue;
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            FilteredCountryList.Clear();
            CountrySuggestions.IsVisible = false;
        }
        else
        {
            var filtered = CountryList
                           .Where(c => c.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                           .ToList();

            FilteredCountryList.Clear();
            foreach (var country in filtered)
            {
                FilteredCountryList.Add(country);
            }

            CountrySuggestions.IsVisible = filtered.Any();
        }
    }

    private void OnCountrySelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is string selectedCountry)
        {
            CountryEntry.Text = selectedCountry;

            SelectedCountry = selectedCountry;

            CountrySuggestions.IsVisible = false;
        }
    }

    private void FilterUniversityButtonClicked(object sender, EventArgs e)
    {
        IEnumerable<University> query = UniversitiesList;

        if (!string.IsNullOrEmpty(StartEntry.Text) && int.TryParse(StartEntry.Text, out int minStudents))
            query = query.Where(n => n.StudentsCount > minStudents);

        if (!string.IsNullOrEmpty(MaxEntry.Text) && int.TryParse(MaxEntry.Text, out int maxStudents))
            query = query.Where(n => n.StudentsCount < maxStudents);

        if (!string.IsNullOrEmpty(SelectedCountry))
            query = query.Where(n => n.Country == SelectedCountry);

        if (!string.IsNullOrEmpty(CityEntry.Text))
            query = query.Where(n => n.City == CityEntry.Text);

        if (!string.IsNullOrEmpty(StartPrice.Text) && int.TryParse(StartPrice.Text, out int minPrice))
            query = query.Where(n => n.Price > minPrice);

        if (!string.IsNullOrEmpty(MaxPrice.Text) && int.TryParse(MaxPrice.Text, out int maxPrice))
            query = query.Where(n => n.Price < maxPrice);
        
        CurrentState = query.ToList();
        var filteredUniversities = new ObservableCollection<University>(CurrentState);
        UniversitiesTable.ItemsSource = filteredUniversities;

        ClearEntries();
    }

    private void DefaultTableButtonClicked(object sender, EventArgs e)
    {
        CurrentState = UniversitiesList;
        UniversitiesTable.ItemsSource = new ObservableCollection<University>(CurrentState);
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

    private void ClearEntries()
    {
        StartEntry.Text = string.Empty;
        MaxEntry.Text = string.Empty;
        CountryEntry.Text = string.Empty;
        CityEntry.Text = string.Empty;
        MaxPrice.Text = string.Empty;
        StartPrice.Text = string.Empty;
    }
}

