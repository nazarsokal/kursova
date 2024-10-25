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

    public static List<StudyField> GetProgramsById(int searchId)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "StudyFields.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        List<string> lines = File.ReadAllLines(filePath).ToList();

        List<StudyField> currentList = null;
        int currentId = -1;

        foreach (var line in lines)
        {
            var idSplit = line.Split(',');
            if (idSplit.Length > 1 && int.TryParse(idSplit[0], out int id))
            {
                if (id != currentId)
                {
                    currentId = id;
                }

                if (id == searchId)
                {
                    currentList ??= new List<StudyField>();

                    var programSplit = idSplit[1].Split(':');
                    if (programSplit.Length < 2) continue;

                    var nameAndPrice = programSplit[0].Split('(');
                    string name = nameAndPrice[0].Trim();
                    double price = double.Parse(nameAndPrice[1].TrimEnd(')'));

                    string description = programSplit[1].Trim();

                    currentList.Add(new StudyField
                    {
                        Id = currentId,
                        FieldName = name,
                        Price = price,
                        Description = description
                    });
                }
                else if (id > searchId && currentList != null)
                {
                    break;
                }
            }
            else if (currentList != null && currentId == searchId)
            {
                var programSplit = line.Split(':');
                if (programSplit.Length < 2) continue;

                var nameAndPrice = programSplit[0].Split('(');
                string name = nameAndPrice[0].Trim();
                double price = double.Parse(nameAndPrice[1].TrimEnd(')'));

                string description = programSplit[1].Trim();

                currentList.Add(new StudyField
                {
                    Id = currentId,
                    FieldName = name,
                    Price = price,
                    Description = description
                });
            }
        }

        return currentList;
    }

    public static void WriteStudyFieldToFile(List<StudyField> studyFields, int id)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "StudyFields.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        foreach (var item in studyFields)
        {
            string strToWrite = $"{id},{item.FieldName}({item.Price}):{item.Description}";
            using (StreamWriter sw = File.AppendText(filePath)) { sw.WriteLine("\n" + strToWrite); }
        }
    }
}