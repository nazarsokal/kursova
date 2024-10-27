using System.Collections.ObjectModel;
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

    public static List<University> SortUniversities(Func<University, University, PropertyInfo, bool> checkSortOrder,string propertyName, List<University> Universities)
    { 
        var newArray = Universities;

        PropertyInfo propertyInfo = typeof(University).GetProperty(propertyName);
        if(propertyInfo == null)
            throw new NullReferenceException("No such Property");

        for (int interval = newArray.Count / 2; interval > 0; interval /= 2) 
        { 
            for (int i = interval; i < Universities.Count; i++) 
            { 
                University temp = newArray[i]; 
                var j = i; 
                
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

    public static void SetUniversities(List<University> universities)
    {
        _universities = universities;
    }
}