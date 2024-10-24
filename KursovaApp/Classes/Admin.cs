
namespace KursovaApp.Classes;

public class Admin : Person
{
    public Admin(string _UserName, string _Email, DateTime _DateRegistred, string _Password, string _IP) 
        : base(_UserName, _Email, _DateRegistred, _Password, _IP)
    {
    }

    public override void LeaveFeedback(University university, Feedback feedback)
    {
        throw new NotImplementedException();
    }
}