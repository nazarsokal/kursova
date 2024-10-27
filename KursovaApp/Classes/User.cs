namespace KursovaApp.Classes;

public class User : Person
{
    public override string Status => "User";
    public User(string _UserName, string _Email, DateTime _DateRegistred, string _Password) 
        : base(_UserName, _Email, _DateRegistred, _Password)
    {

    }
}