using System.Security.Cryptography.X509Certificates;

namespace KursovaApp.Classes;

public class University
{
    public string Name { get; set;}
    public string City { get; set;}
    public string Country { get; set;}
    public int StudentsCount { get; set;}
    public List<string> StudyFields { get; }
    public double Price { get; set;}

    private readonly string FilePath = "/Users/asokalch/Documents/GitHub/kursova/KursovaApp/Classes/UniversityList";

    public University(string _Name, string _City, string _Country, int _StudentsCount, List<string> _StudyField, double _Price)
    {
        Name = _Name;
        City = _City;
        Country = _Country;
        StudentsCount = _StudentsCount;
        StudyFields = _StudyField;
        Price = _Price;
    }
    public University(string _Name, string _City, string _Country, int _StudentsCount, double _Price)
    {
        Name = _Name;
        City = _City;
        Country = _Country;
        StudentsCount = _StudentsCount;
        Price = _Price;
    }



    public University()
    {

    }

    public void FindUniversityByCountryCity(string countryFromUser, string cityFromUser)
    {
        
    }

    public List<University> ReadFile()
    {
        var universities = new List<University>();
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "UniversityList.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        if (!Directory.Exists(subFolderPath))
        {
            Directory.CreateDirectory(subFolderPath);  // This will create the "kursova/KursovaApp" directory if it doesn't exist
        }

        var lines = File.ReadAllLines(filePath);
        
        for (int i = 0; i < lines.Count(); i++)
        {
            List<string> lineArray = lines.ToList()[i].Split(',').ToList();
            List<string> studyFields = lineArray[4]
                .Split(':')
                .Select(field => field.Replace("{", "").Replace("}", ""))
                .ToList();

            universities.Add(new University(_Name: lineArray[0], _City: lineArray[1], _Country: lineArray[2], _StudentsCount: int.Parse(lineArray[3]), 
            _StudyField: studyFields, _Price: Price));
        }

        return universities;
    }
}