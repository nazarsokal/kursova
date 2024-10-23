
namespace KursovaApp.Classes;

public class Admin : Person
{
    public Admin(string _UserName, string _Email, DateTime _DateRegistred, string _Password) 
        : base(_UserName, _Email, _DateRegistred, _Password)
    {
    }

    public override void LeaveFeedback()
    {
        throw new NotImplementedException();
    }
}