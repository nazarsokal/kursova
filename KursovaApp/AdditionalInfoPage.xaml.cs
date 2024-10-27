using KursovaApp.Classes;

namespace KursovaApp;

public partial class AdditionalInfoPage : ContentPage
{
    private List<Feedback> feedbacksList = new List<Feedback>();
    public Person LoggedPerson { get; set; }
    private readonly University SelectedUniversity;
    public AdditionalInfoPage(University _university, Person person)
    {
        InitializeComponent();

        SelectedUniversity = _university;
        LoggedPerson = person;

        BindingContext = SelectedUniversity;

        UniPicture.Source = SelectedUniversity.PhotoPath;

        //StudyFieldsLabel.Text = university.StudyFields.Aggregate("", (current, s) => current + ("•" + s.Name + "\t"));
        StudyFieldsCollection.ItemsSource = SelectedUniversity.StudyFields;

        NameLabel.Text = LoggedPerson.UserName;

        UpdateTable();
    }

    private void UpdateTable()
    {
        feedbacksList = FileRepository.ReadFeedbacksFromFile(SelectedUniversity);
        CommentsCollectionView.ItemsSource = feedbacksList.OrderByDescending(n => n.PublishDate);
    }

    private void PublishCommentButton_Clicked(object sender, EventArgs e)
    {
        var message = CommentEditor.Text;

        Feedback feedbackToWrite = new Feedback(SelectedUniversity.Name, LoggedPerson.UserName, DateTime.Now, message);

        FileRepository.WriteFeedbackToFile(SelectedUniversity, feedbackToWrite);
        UpdateTable();
    }
}