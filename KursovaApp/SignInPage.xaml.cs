using KursovaApp.Classes;

namespace KursovaApp;

public partial class SignInPage : ContentPage
{
    public SignInPage()
    {
        InitializeComponent();
    }

    async private void SignInButton_Pressed(object sender, EventArgs e)
    {
        var UsernameEntered = UsernameEntry.Text;
        var PasswordEntered = PasswordEntry.Text;

        var personList = Person.ReadFromFile();
        if(personList.Any(n => n.UserName == UsernameEntered && n.Password == PasswordEntered))
        {
            Person person = personList.Find(n => n.UserName == UsernameEntered && n.Password == PasswordEntered);
            if(person?.Status == "Admin")
            {
                Admin admin = person as Admin;
                await Navigation.PushAsync(new AdminMainPage() {_Admin = admin});
            }
            else if(person?.Status == "User")
            {
                User user = person as User;
                await Navigation.PushAsync(new MainPage() {_User = user});
            }
        }
        else
            await DisplayAlert("Ok", "Hyjna", "ok");
    }

}