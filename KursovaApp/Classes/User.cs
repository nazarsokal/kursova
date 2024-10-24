namespace KursovaApp.Classes;

public class User : Person
{
    public User(string _UserName, string _Email, DateTime _DateRegistred, string _Password, string _IP) 
        : base(_UserName, _Email, _DateRegistred, _Password, _IP)
    {

    }

    public override void LeaveFeedback(University university, Feedback feedback)
    {
        FeedbackRepository.WriteFeedbackToFile(university, feedback);
    }

    public void RegisterUserToFile(string status)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "Users.txt";

        string filePath = Path.Combine(subFolderPath, fileName);
        string strToWrite = $"{status};{this.UserName};{this.Email};{this.DateRegistred};{this.Password};{GetIp()}";

        using (StreamWriter sw = File.AppendText(filePath)) { sw.WriteLine(strToWrite); }
    }
}