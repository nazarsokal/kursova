using System.Collections.ObjectModel;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class AdminMainPage : ContentPage
{
    public Admin _Admin { get; set; }
    public ObservableCollection<University> Universities { get; set; }
    public List<University> UniversitiesList { get; set; }
    public UniversityRepository UniversityRepository { get; private set; }
	private bool isSecondClick = false;
    
    public AdminMainPage()
    {
        InitializeComponent();

		UniversitiesList = FileRepository.ReadFile();
        Universities = new ObservableCollection<University>(UniversitiesList);
        UniversityRepository = new UniversityRepository(UniversitiesList);

		UniversitiesTable.ItemsSource = Universities;
    }
    private void AddUniversityButton_Clicked(object sender, EventArgs e)
    {
		var secondWindow = new AddUniversityPage(UniversitiesList, _Admin);
		var spWindow = new Window(secondWindow);

		Application.Current?.OpenWindow(spWindow);
    }
    private void EditUniversityButtonClicked(object sender, EventArgs e)
    {
		University ChosenUniversity = new University();
		if (!isSecondClick)
		{
			UniversityToEdit.IsVisible = !UniversityToEdit.IsVisible;
			isSecondClick = true;
		}
		else
		{
			ChosenUniversity = UniversitiesList.Find(n => n.Name == UniversityToEdit.Text);
			int Id = UniversitiesList.IndexOf(ChosenUniversity);
			var secondWindow = new EditUniversityPage(ChosenUniversity, Id);
			var spWindow = new Window(secondWindow);
	
			Application.Current?.OpenWindow(spWindow);

			isSecondClick = false;
		}
    }

    
}