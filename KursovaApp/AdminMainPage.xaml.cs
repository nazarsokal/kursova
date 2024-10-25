using System.Collections.ObjectModel;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class AdminMainPage : ContentPage
{
    public Admin _Admin { get; set; }
    public ObservableCollection<University> Universities { get; set; }
    public List<University> UniversitiesList { get; set; }
    public UniversityRepository UniversityRepository { get; private set; }
    
    public AdminMainPage()
    {
        InitializeComponent();

        Universities = new ObservableCollection<University>();

		UniversitiesList = UniversityRepository.ReadFile();
        UniversityRepository = new UniversityRepository(UniversitiesList);

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
    }

    
}