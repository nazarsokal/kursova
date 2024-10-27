using System.Net;
using System.Net.Sockets;

namespace KursovaApp.Classes;

public class Person
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateRegistred { get; set; }
    public virtual string Status { get; set; }
    public Person(string _UserName, string _Email, DateTime _DateRegistred, string _Password)
    {
        UserName = _UserName;
        Email = _Email;
        Password = _Password;
        DateRegistred = _DateRegistred;
    }

    public virtual void LeaveFeedback(University university, Feedback feedback) {}

}