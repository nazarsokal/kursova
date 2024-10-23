namespace KursovaApp.Classes;

public class User : Person
{
    public override User(string _UserName, string _Email, DateTime _DateRegistred, string _Password) 
        : base(_UserName, _Email, _DateRegistred, _Password)
    {

    }

    public override void LeaveFeedback(Feedback feedback)
    {
        FeedbackRepository.WriteFeedbackToFile(feedback);
    }
}