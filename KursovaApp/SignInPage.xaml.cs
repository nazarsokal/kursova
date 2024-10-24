using KursovaApp.Classes;

namespace KursovaApp;

public partial class SignInPage : ContentPage
{
    public SignInPage()
    {
        InitializeComponent();
    }

    async private void RegisterButton_Pressed(object sender, EventArgs e)
    {
        var UsernameEntered = UsernameEntry.Text;
        var PasswordEntered = PasswordEntry.Text;

        var personList = Person.ReadFromFile();
        if(personList.Any(n => n.UserName == UsernameEntered && n.Password == PasswordEntered))
        {
            Person? person = personList.Find(n => n.UserName == UsernameEntered && n.Password == PasswordEntered);
            if(person?.Status == "Admin")
            {
                await DisplayAlert("Ok", "You are admin", "Ok");
            }
            else if(person?.Status == "User")
            {
                await Navigation.PushAsync(new MainPage());
            }
        }
    }

}