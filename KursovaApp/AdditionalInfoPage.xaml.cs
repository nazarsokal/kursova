using KursovaApp.Classes;

namespace KursovaApp;

public partial class AdditionalInfoPage : ContentPage
{
    public AdditionalInfoPage(University university)
    {
        InitializeComponent();

        BindingContext = university;

        UniPicture.Source = university.PhotoPath;

        //StudyFieldsLabel.Text = university.StudyFields.Aggregate("", (current, s) => current + ("•" + s.Name + "\t"));
        StudyFieldsCollection.ItemsSource = university.StudyFields;
    }
}