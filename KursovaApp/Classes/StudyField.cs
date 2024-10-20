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

        // Зчитуємо всі рядки з файлу
        List<string> lines = File.ReadAllLines(filePath).ToList();

        List<StudyField> currentList = null;
        int currentId = -1;

        foreach (var line in lines)
        {
            // Перевіряємо, чи рядок починається з нового ID
            var idSplit = line.Split(',');
            if (idSplit.Length > 1 && int.TryParse(idSplit[0], out int id))
            {
                if (id != currentId)
                {
                    // Якщо id змінився, оновлюємо поточний id
                    currentId = id;
                }

                // Якщо це ID, яке ми шукаємо, створюємо новий список
                if (id == searchId)
                {
                    currentList ??= new List<StudyField>(); // ініціалізуємо список, якщо він ще не існує

                    // Розділяємо частину з даними на ім'я програми, ціну і опис
                    var programSplit = idSplit[1].Split(':');
                    if (programSplit.Length < 2) continue;

                    // Отримуємо ім'я і ціну програми
                    var nameAndPrice = programSplit[0].Split('(');
                    string name = nameAndPrice[0].Trim();
                    double price = double.Parse(nameAndPrice[1].TrimEnd(')'));

                    // Отримуємо опис програми
                    string description = programSplit[1].Trim();

                    // Додаємо новий об'єкт StudyFields до поточного списку
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
                    // Якщо ми перейшли до наступного ID після пошуку, закінчуємо пошук
                    break;
                }
            }
            else if (currentList != null && currentId == searchId)
            {
                // Якщо продовжується опис для поточного ID, який шукаємо
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

        return currentList; // Повертаємо список для вказаного ID
    }
}