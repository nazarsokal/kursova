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
            return _universities;

        return universities;
    }
}