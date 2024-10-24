using System.Security.Cryptography.X509Certificates;

namespace KursovaApp.Classes;

public class University : IDescriptionable
{
    public string Name { get; set;}
    public string City { get; set;}
    public string Country { get; set;}
    public int StudentsCount { get; set;}
    public List<StudyField> StudyFields { get; set;}
    public double Price { get; set;}
    public string Description { get; set; }

    public string PhotoPath { get; set; }

    public University(string _Name, string _City, string _Country, int _StudentsCount, List<StudyField> _StudyField, double _Price, string _Description, string _PhotoPath)
    {
        Name = _Name;
        City = _City;
        Country = _Country;
        StudentsCount = _StudentsCount;
        StudyFields = _StudyField;
        Price = _Price;
        Description = _Description;
        PhotoPath = _PhotoPath;
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
}