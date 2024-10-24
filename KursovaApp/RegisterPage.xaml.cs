using KursovaApp.Classes;

namespace KursovaApp;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    async void OnRegisterClicked(object sender, EventArgs e)
    {
        if(UsernameEntry.Text != null && EmailEntry.Text != null && PasswordEntry.Text != null && ConfirmPasswordEntry.Text != null)
        {
            if(PasswordEntry.Text == ConfirmPasswordEntry.Text)
            {
                if(!ContainsTheSameUserName(UsernameEntry.Text) && !ContainsTheSameEmail(EmailEntry.Text))
                {
                    User person = new User(UsernameEntry.Text, PasswordEntry.Text, DateTime.Now, EmailEntry.Text, User.GetIp());
                    person.RegisterUserToFile("User");
                    await DisplayAlert("Success", "Ви успішно зареєструвалися у системі", "OK");
                }
                else
                    await DisplayAlert("Fail", "Користувач з таким логіном або поштою уже зареєстрований", "OK");
            }
            else
                await DisplayAlert("Fail", "Паролі повинні співпадати", "OK");

        }
    }

    public bool ContainsTheSameUserName(string userName) => Person.ReadFromFile().Any(n => n.UserName == userName);
    public bool ContainsTheSameEmail(string email) => Person.ReadFromFile().Any(n => n.Email == email);
}