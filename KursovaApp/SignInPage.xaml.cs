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

        var personList = FileRepository.ReadFromFile();
        Person? person = personList.Find(n => n.UserName == UsernameEntered && n.Password == PasswordEntered);

        try
        {
            if (person != null)
            {
                if (person.Status == "Admin")
                {
                    await Navigation.PushAsync(new AdminMainPage() { _Admin = new Admin(person.UserName, person.Email, person.DateRegistred, person.Password) });
                }
                else if (person.Status == "User")
                {
                    await Navigation.PushAsync(new MainPage() { _User = new User(person.UserName, person.Email, person.DateRegistred, person.Password) });
                }
                else
                {
                    await DisplayAlert("Error", "Invalid user status.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

}