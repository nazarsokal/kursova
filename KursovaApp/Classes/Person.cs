using System.Net;
using System.Net.Sockets;

namespace KursovaApp.Classes;

public class Person
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateRegistred { get; set; }
    public string Status { get; set; }
    public string IP { get; set; }
    public Person(string _UserName, string _Email, DateTime _DateRegistred, string _Password, string _IP)
    {
        UserName = _UserName;
        Email = _Email;
        Password = _Password;
        DateRegistred = _DateRegistred;
        IP = _IP;
    }

    public virtual void LeaveFeedback(University university, Feedback feedback) {}

    public static string GetIp()
    {
        string hostName = Dns.GetHostName(); // Get the host name

        // Get the list of IP addresses associated with the host
        IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

        foreach (IPAddress ip in ipAddresses)
        {
            // Return the first IPv4 address found
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return "";
    }

    public static List<Person> ReadFromFile()
    {
        var result = new List<Person>();
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "Users.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        var line = File.ReadAllLines(filePath);
        for (int i = 0; i < line.Count(); i++)
        {
            List<string> lineArray = line[i].Split(';').ToList();

            result.Add(new Person(lineArray[1], lineArray[2], DateTime.Parse(lineArray[3]), lineArray[4], GetIp()) {Status = lineArray[0]});
        }

        return result;
    }
}