namespace KursovaApp.Classes;

public class Feedback
{
    public string UniversityName { get; set; }
    public string Username { get; set; }
    public DateTime PublishDate { get; set; }
    public string Message { get; set; }

    public Feedback(string _universityName, string _username, DateTime _publishDate, string _message)
    {
        UniversityName = _universityName;
        Username = _username;
        PublishDate = _publishDate;
        Message = _message;
    }

    public Feedback() { }

}