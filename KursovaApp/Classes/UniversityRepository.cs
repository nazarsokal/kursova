using System.Reflection;

namespace KursovaApp.Classes;

public class UniversityRepository
{
    private static List<University> _universities;

    public UniversityRepository(List<University> universities)
    {
        _universities = universities;
    }

    public static List<University> SearchUnivesities(string filterText)
    {

        if (string.IsNullOrWhiteSpace(filterText))
            return _universities;

        var universities = _universities.
            Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if(universities == null || universities.Count < 1)
            return new List<University>();

        return universities;
    }

    public static List<University> StudentsCountUniversities(int minCount, int maxCount)
    {
        List<University> specialCountUniversities = _universities.
				Where(sc => sc.StudentsCount > minCount && sc.StudentsCount < maxCount)
				.ToList();
	
        if(specialCountUniversities.Count() == 0)
            return new List<University>();
        else
            return specialCountUniversities;
    }

    public static List<University> ReadFile()
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
            List<StudyField> studyFields = StudyField.GetProgramsById(int.Parse(lineArray[0]));

            double averagePrice = studyFields.Sum(n => n.Price) / studyFields.Count;

            universities.Add(new University(_Name: lineArray[1], _City: lineArray[2], _Country: lineArray[3], _StudentsCount: int.Parse(lineArray[4]), 
            _StudyField: studyFields, _Price: averagePrice));
        }

        return universities;
    }

    public static List<University> SortUniversities(Func<University, University, PropertyInfo, bool> checkSortOrder,string propertyName)
    { 
        var newArray = _universities;

        PropertyInfo propertyInfo = typeof(University).GetProperty(propertyName);
        if(propertyInfo == null)
            throw new NullReferenceException("No such Property");

        // Shell sort with decreasing interval
        for (int interval = newArray.Count / 2; interval > 0; interval /= 2) 
        { 
            for (int i = interval; i < _universities.Count; i++) 
            { 
                University temp = newArray[i]; 
                var j = i; 
                
                // Sort in ascending order, so check for greater values to move
                while (j >= interval && checkSortOrder(newArray[j - interval], temp, propertyInfo)) 
                { 
                    newArray[j] = newArray[j - interval]; 
                    j -= interval; 
                } 
                newArray[j] = temp; 
            } 
        } 

        return newArray;
    }

    private static int PropertyCompare(University un1, University un2, PropertyInfo propertyInfo)
    {
        var un1Value = propertyInfo.GetValue(un1);
        var un2Value = propertyInfo.GetValue(un2);

        if (un1Value is IComparable comparableA && un2Value is IComparable comparableB)
        {
            return comparableA.CompareTo(comparableB);
        }

        return 0;
    }

    public static bool predASC(University un1, University un2, PropertyInfo propertyInfo) => PropertyCompare(un1, un2, propertyInfo) > 0;
    public static bool predDESC(University un1, University un2, PropertyInfo propertyInfo) => PropertyCompare(un1, un2, propertyInfo) < 0;
}