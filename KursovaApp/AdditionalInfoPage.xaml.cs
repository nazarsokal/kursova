using KursovaApp.Classes;

namespace KursovaApp;

public partial class AdditionalInfoPage : ContentPage
{
    private List<Feedback> feedbacksList = new List<Feedback>();
    public AdditionalInfoPage(University university)
    {
        InitializeComponent();

        BindingContext = university;

        UniPicture.Source = university.PhotoPath;

        //StudyFieldsLabel.Text = university.StudyFields.Aggregate("", (current, s) => current + ("•" + s.Name + "\t"));
        StudyFieldsCollection.ItemsSource = university.StudyFields;

        feedbacksList = FeedbackRepository.ReadFeedbacksFromFile(university);
        UpdateTable();
    }

    private void UpdateTable()
    {
        CommentsCollectionView.ItemsSource = feedbacksList.OrderByDescending(n => n.PublishDate);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
    }
}