using KursovaApp.Classes;

namespace KursovaApp;

public partial class EditUniversityPage : ContentPage
{
    public List<StudyField> StudyFields { get; set; }
    public University ChosenUniversity { get; set; }  
    public int Id { get; set; }  
    public EditUniversityPage(University university, int id)
    {
        InitializeComponent();

        ChosenUniversity = university;
        Id = id;

        BindingContext = university;
    }
    private void OnSaveUniversityClicked(object sender, EventArgs e)
    {
        var UniversityName = UniversityNameEntry.Text;
        var UniversityCity = CityEntry.Text;
        var UniversityDescription = DescriptionEntry.Text;
        var UniversityStudentsCount = StudentCountStepper.Value;
        var UniversityCountry = CountryEntry.Text;
        var UniversityPrice = ChosenUniversity.StudyFields.Sum(p => p.Price) / ChosenUniversity.StudyFields.Count;
        var photoPath = ChosenUniversity.PhotoPath;

        var UniversityEdited = new University(UniversityName, UniversityCity, UniversityCountry, (int)UniversityStudentsCount, UniversityPrice, UniversityDescription);

        FileRepository.EditUniversity(ChosenUniversity, UniversityEdited, Id);
        FileRepository.UpdateStudyFields(ChosenUniversity.StudyFields, Id);

        DisplayAlert("Успішно", "Ви успішно змінили університет", "Ok");
    }

    private void OnAddSpecialtyClicked(object sender, EventArgs e)
    {
        var StudyFieldName = SpecialtyNameEntry.Text;
        var StudyFieldDescription = SpecialtyDescriptionEditor.Text;
        var StudyFieldPrice = SpecialtyPriceStepper.Value;

        ChosenUniversity.StudyFields.Add(new StudyField(StudyFieldName, StudyFieldPrice, StudyFieldDescription));

        SpecialtyNameEntry.Text = string.Empty;
        SpecialtyDescriptionEditor.Text = string.Empty;
        SpecialtyPriceStepper.Value = 0;
    }
}