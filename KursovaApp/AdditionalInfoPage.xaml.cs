using KursovaApp.Classes;

namespace KursovaApp;

public partial class AdditionalInfoPage : ContentPage
{
    private List<Feedback> feedbacksList = new List<Feedback>();
    private readonly University SelectedUniversity;
    public AdditionalInfoPage(University _university)
    {
        InitializeComponent();

        SelectedUniversity = _university;

        BindingContext = SelectedUniversity;

        UniPicture.Source = SelectedUniversity.PhotoPath;

        //StudyFieldsLabel.Text = university.StudyFields.Aggregate("", (current, s) => current + ("•" + s.Name + "\t"));
        StudyFieldsCollection.ItemsSource = SelectedUniversity.StudyFields;

        UpdateTable();
    }

    private void UpdateTable()
    {
        feedbacksList = FeedbackRepository.ReadFeedbacksFromFile(SelectedUniversity);
        CommentsCollectionView.ItemsSource = feedbacksList.OrderByDescending(n => n.PublishDate);
    }

    private void PublishCommentButton_Clicked(object sender, EventArgs e)
    {
        var userName = NameEntry.Text;
        var message = CommentEditor.Text;

        Feedback feedbackToWrite = new Feedback(SelectedUniversity.Name, userName, DateTime.Now, message);

        FeedbackRepository.WriteFeedbackToFile(SelectedUniversity, feedbackToWrite);
        UpdateTable();
    }
}