using System.Security.Cryptography.X509Certificates;

namespace StudyNET;

public class University
{
    public string Name { get; }
    public string City { get; }
    public string Country { get; }
    public int StudentsCount { get; }
    public List<string> StudyFields { get; }
    public double Price { get; }

    private readonly string filePath = "/Users/asokalch/Documents/StudyNETproj/StudyProject/UniversityList";
    private int wordsCount;

    public University(string _Name, string _City, string _Country, int _StudentsCount, List<string> _StudyField, double _Price)
    {
        Name = _Name;
        City = _City;
        Country = _Country;
        StudentsCount = _StudentsCount;
        StudyFields = _StudyField;
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
        string line = null;
        string[] fields = null;
        var delim = ",";
        var universities = new List<University>();
        using(var streamReader = new StreamReader(filePath))
        {
            var lines = File.ReadLines(filePath);
            int counter = 0;
            
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
        };
    }
}