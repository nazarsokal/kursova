namespace KursovaApp.Classes;

public static class FileRepository
{
    public static List<University> ReadFile()
    {
        var universities = new List<University>();
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "UniversityList.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        string subPhotoFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp", "Photos");

        if (!Directory.Exists(subFolderPath))
        {
            Directory.CreateDirectory(subFolderPath);  // This will create the "kursova/KursovaApp" directory if it doesn't exist
        }

        var lines = File.ReadAllLines(filePath);
        
        for (int i = 0; i < lines.Count(); i++)
        {
            List<string> lineArray = lines.ToList()[i].Split(';').ToList();
            List<StudyField> studyFields = GetProgramsById(int.Parse(lineArray[0]));

            double averagePrice = studyFields.Sum(n => n.Price) / studyFields.Count;
            string photoName = $"uni{i}.jpg";
            string photoPath = Path.Combine(subPhotoFolderPath, photoName);

            universities.Add(new University(_Name: lineArray[1], _City: lineArray[2], _Country: lineArray[3], _StudentsCount: int.Parse(lineArray[4]), 
            _StudyField: studyFields, _Price: averagePrice, _Description: lineArray[5], _PhotoPath: photoPath));
        }

        return universities;
    }

    public static void WriteUniversityToFile(University university, int id)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "UniversityList.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        string strToWrite = $"{id};{university.Name};{university.City};{university.Country};{university.StudentsCount};{university.Description}";

        using (StreamWriter sw = File.AppendText(filePath)) { sw.WriteLine("\n" + strToWrite); }
    }

    public static void EditUniversity(University universityToEdit, University universityEdited, int id)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "UniversityList.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        List<string> lines = File.ReadAllLines(filePath).ToList();
        int indexToReplace = lines.FindIndex(line => line.Contains(universityToEdit.Name));

        if (indexToReplace != -1)
        {
            lines[indexToReplace] = $"{id};{universityEdited.Name};{universityEdited.City};{universityEdited.Country};{universityEdited.StudentsCount};{universityEdited.Description}";

            File.WriteAllLines(filePath, lines);
        }
    }
    public static List<Feedback> ReadFeedbacksFromFile(University university)
    {
        var feedbacks = new List<Feedback>();
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "Feedbacks.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        var lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Count(); i++)
        {
            List<string> lineArray = lines[i].Split(';').ToList();
            if(lineArray[0] == university.Name)
            {
                feedbacks.Add(new Feedback() {UniversityName = lineArray[0], Username =lineArray[1], PublishDate = DateTime.Parse(lineArray[2]), Message = lineArray[3]});
            }
        }

        return feedbacks;
    }

    public static void WriteFeedbackToFile(University university, Feedback feedback)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "Feedbacks.txt";

        string filePath = Path.Combine(subFolderPath, fileName);
        string strToWrite = $"{university.Name};{feedback.Username};{feedback.PublishDate};{feedback.Message}";

        using (StreamWriter sw = File.AppendText(filePath)) { sw.WriteLine(strToWrite); }
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

            result.Add(new Person(lineArray[1], lineArray[2], DateTime.Parse(lineArray[3]), lineArray[4]) {Status = lineArray[0]});
        }

        return result;
    }

    public static void RegisterUserToFile(User user ,string status)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "Users.txt";

        string filePath = Path.Combine(subFolderPath, fileName);
        string strToWrite = $"{status};{user.UserName};{user.Email};{user.DateRegistred};{user.Password}";

        using (StreamWriter sw = File.AppendText(filePath)) { sw.WriteLine("\n"+strToWrite); }
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
            using (StreamWriter sw = File.AppendText(filePath)) { sw.WriteLine("\n"+strToWrite); }
        }
    }

    public static void UpdateStudyFields(List<StudyField> studyFields, int id)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string subFolderPath = Path.Combine(desktopPath, "kursova", "KursovaApp");
        string fileName = "StudyFields.txt";

        string filePath = Path.Combine(subFolderPath, fileName);

        List<string> lines = File.Exists(filePath) ? File.ReadAllLines(filePath).ToList() : new List<string>();

        string newFieldsData = string.Join(", ", studyFields.Select(field => $"{field.FieldName}({field.Price}):{field.Description}"));

        int indexToUpdate = lines.FindIndex(line => line.StartsWith($"{id},"));

        if (indexToUpdate != -1)
        {
            lines[indexToUpdate] += $", {newFieldsData}";
        }
        else
        {
            lines.Add($"{id},{newFieldsData}");
        }
        File.WriteAllLines(filePath, lines);
    }

}