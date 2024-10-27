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
                    User user = new User(UsernameEntry.Text, EmailEntry.Text, DateTime.Now, PasswordEntry.Text);
                    FileRepository.RegisterUserToFile(user, "User");
                    await DisplayAlert("Success", "Ви успішно зареєструвалися у системі", "OK");
                }
                else
                    await DisplayAlert("Fail", "Користувач з таким логіном або поштою уже зареєстрований", "OK");
            }
            else
                await DisplayAlert("Fail", "Паролі повинні співпадати", "OK");
        }
        else
            await DisplayAlert("Fail", "Bluat", "OK");
    }

    public bool ContainsTheSameUserName(string userName) => FileRepository.ReadFromFile().Any(n => n.UserName == userName);
    public bool ContainsTheSameEmail(string email) => FileRepository.ReadFromFile().Any(n => n.Email == email);

    async private void OnLabelTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignInPage());
    }
}