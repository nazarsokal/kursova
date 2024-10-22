namespace KursovaApp.Classes;

public static class FeedbackRepository
{
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

    
}