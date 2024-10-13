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
}