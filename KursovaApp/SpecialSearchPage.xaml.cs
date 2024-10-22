using System.Collections.ObjectModel;
using KursovaApp.Classes;

namespace KursovaApp;

public partial class SpecialSearchPage : ContentPage
{
    public ObservableCollection<string> Countries { get; set; }

    public SpecialSearchPage(List<University> universitiesList)
    {
        InitializeComponent();

        // Populate Countries list
        var selectedCountries = SelectCountries(universitiesList);

        foreach (var item in selectedCountries)
        {
            Countries.Add(item);
        }

        // Set the BindingContext to this page so that XAML can access the properties
        BindingContext = this;
    }

    private List<string> SelectCountries(List<University> universities)
    {
        var result = new ObservableCollection<string>();
        foreach (var item in universities)
        {
            result.Add(item.Country);
        }

        return result.Distinct().ToList();
    }
}
