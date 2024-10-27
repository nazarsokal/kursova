
using System.Diagnostics;

namespace KursovaApp.Classes;

public class Admin : Person
{
    public override string Status => "Admin";

    public Admin(string _UserName, string _Email, DateTime _DateRegistred, string _Password) 
        : base(_UserName, _Email, _DateRegistred, _Password)
    {
    }

    public override void LeaveFeedback(University university, Feedback feedback)
    {
        throw new NotImplementedException();
    }

    public void AddUniversity(string UniversityName, string UniversityCity, string UniversityCountry, int UniversityStudentsCount, 
                            string UniversityDescription, int UniversityPrice, int id, List<StudyField> studyFields)
    {
       try
       {
         var UniversityToAdd = new University(UniversityName, UniversityCity, UniversityCountry, UniversityStudentsCount, UniversityPrice, UniversityDescription);
 
         FileRepository.WriteUniversityToFile(UniversityToAdd, id);
         FileRepository.WriteStudyFieldToFile(studyFields, id);
 
       }
       catch (System.Exception ex)
       {
        Debug.WriteLine(ex.Message);
       }
    }

    public void EditUniversity(University ChosenUniversity, string UniversityName, string UniversityCity, string UniversityCountry, int UniversityStudentsCount, 
                            string UniversityDescription, int UniversityPrice, int id)
    {
        var UniversityEdited = new University(UniversityName, UniversityCity, UniversityCountry, (int)UniversityStudentsCount, UniversityPrice, UniversityDescription);

        FileRepository.EditUniversity(ChosenUniversity, UniversityEdited, id);
        FileRepository.UpdateStudyFields(ChosenUniversity.StudyFields, id);
    }
}