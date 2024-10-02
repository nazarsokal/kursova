using KursovaApp.Classes;

namespace KursovaApp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnButtonClicked(object sender, EventArgs e)
	{
		University university = new University();

		university.ReadFile();
	}
}

