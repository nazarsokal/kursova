using KursovaApp.Classes;

namespace KursovaApp;

public partial class EditUniversityPage : ContentPage
{
    public List<StudyField> StudyFields { get; set; }
    public University ChosenUniversity { get; set; }  
    public int Id { get; set; }  
    public Admin AdminWhoEdited { get; private set; }
    public EditUniversityPage(University university, int id, Admin admin)
    {
        InitializeComponent();

        ChosenUniversity = university;
        Id = id;
        AdminWhoEdited = admin;

        BindingContext = university;
    }
    private void OnSaveUniversityClicked(object sender, EventArgs e)
    {
        var UniversityName = UniversityNameEntry.Text;
        var UniversityCity = CityEntry.Text;
        var UniversityDescription = DescriptionEntry.Text;
        var UniversityStudentsCount = StudentCountStepper.Value;
        var UniversityCountry = CountryEntry.Text;
        int UniversityPrice = (int)ChosenUniversity.StudyFields.Sum(p => p.Price) / ChosenUniversity.StudyFields.Count;
        var photoPath = ChosenUniversity.PhotoPath;

        AdminWhoEdited.EditUniversity(ChosenUniversity, UniversityNameEntry.Text, CityEntry.Text, CountryEntry.Text, (int)StudentCountStepper.Value, DescriptionEntry.Text, UniversityPrice, Id);

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