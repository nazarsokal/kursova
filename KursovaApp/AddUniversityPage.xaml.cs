using KursovaApp.Classes;
using UIKit;

namespace KursovaApp;

public partial class AddUniversityPage : ContentPage
{
    public List<University> UniversitiesList { get; private set; }
    private List<StudyField> StudyFields = new List<StudyField>();
    public string PhotoPath { get; private set; }
    public AddUniversityPage(List<University> universities)
    {
        InitializeComponent();

        UniversitiesList = universities;
    }

    private void OnSaveUniversityClicked(object sender, EventArgs e)
    {
        if(StudyFields != null)
        {
            var UniversityName = UniversityNameEntry.Text;
            var UniversityCity = CityEntry.Text;
            var UniversityDescription = DescriptionEntry.Text;
            var UniversityStudentsCount = StudentCountStepper.Value;
            var UniversityCountry = CountryEntry.Text;
            var UniversityPrice = StudyFields.Sum(p => p.Price) / StudyFields.Count;
            var photoPath = CopyPhoto(PhotoPath, UniversitiesList.Count);

            var UniversityToAdd = new University(UniversityName, UniversityCity, UniversityCountry, (int)UniversityStudentsCount, UniversityPrice, UniversityDescription);

            UniversityRepository.WriteUniversityToFile(UniversityToAdd, UniversitiesList.Count);
            StudyField.WriteStudyFieldToFile(StudyFields, UniversitiesList.Count);

            DisplayAlert("Ok", "Ви успішно додали університет", "Ok");
        }
        else
            DisplayAlert("Ok", "0 study fields", "Ok");
    }

    private void OnAddSpecialtyClicked(object sender, EventArgs e)
    {
        var StudyFieldName = SpecialtyNameEntry.Text;
        var StudyFieldDescription = SpecialtyDescriptionEditor.Text;
        var StudyFieldPrice = SpecialtyPriceStepper.Value;

        StudyFields.Add(new StudyField(StudyFieldName, StudyFieldPrice, StudyFieldDescription));

        SpecialtyNameEntry.Text = string.Empty;
        SpecialtyDescriptionEditor.Text = string.Empty;
        SpecialtyPriceStepper.Value = 0;
    }
    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Виберіть фото університету",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                PhotoPath = result.FullPath;

                var stream = await result.OpenReadAsync();
                UniversityImage.Source = ImageSource.FromStream(() => stream);
                UniversityImage.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions here
            await DisplayAlert("Помилка", "Не вдалося завантажити фото", "OK");
        }
    }

    private string CopyPhoto(string currentPath, int id)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp", "Photos");
        string photoName = $"uni{id}.jpg";
        string photoPath = Path.Combine(subFolderPath, photoName);

        File.Move(currentPath, photoPath);
        return photoPath;
    }

    private void CloseWindow()
    {
        var windows = UIApplication.SharedApplication.Windows;
    
        foreach (var window in windows)
        {
            if (window == UIApplication.SharedApplication.KeyWindow) // Ensure it's not the main window
            {
                window.Hidden = true; // This will close the window
                break; // Exit after closing the desired window
            }
        }
    }
}