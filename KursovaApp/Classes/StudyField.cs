namespace KursovaApp.Classes;

public class StudyField : IDescriptionable
{
    public int Id { get; set; }
    public string FieldName { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }

    public StudyField(string _Name, double _Price, string _Description)
    {
        FieldName = _Name;
        Price = _Price;
        Description = _Description;
    }

    public StudyField()
    {

    }
}