using KursovaApp.Classes;
using UIKit;

namespace KursovaApp;

public partial class AddUniversityPage : ContentPage
{
    public List<University> UniversitiesList { get; private set; }
    private List<StudyField> StudyFields = new List<StudyField>();
    public string PhotoPath { get; private set; }
    public Admin AdminWhoAdded { get; set; }
    public AddUniversityPage(List<University> universities, Admin _admin)
    {
        InitializeComponent();

        UniversitiesList = universities;
        AdminWhoAdded = _admin;
    }

    private void OnSaveUniversityClicked(object sender, EventArgs e)
    {
        if(StudyFields != null)
        {
            try
            {
                var UniversityPrice = StudyFields.Sum(p => p.Price) / StudyFields.Count;
                var photoPath = CopyPhoto(PhotoPath, UniversitiesList.Count);

                AdminWhoAdded.AddUniversity(UniversityNameEntry.Text, CityEntry.Text, CountryEntry.Text, (int)StudentCountStepper.Value,
                    DescriptionEntry.Text, UniversityPrice, UniversitiesList.Count, StudyFields);

                DisplayAlert("Ok", "Ви успішно додали університет", "Ok");
            }
            catch (System.Exception ex)
            {
                DisplayAlert("Ok", ex.Message + "\n" + ex.Source, "Ok");
            }
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