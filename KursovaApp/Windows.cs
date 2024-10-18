namespace KursovaApp;

public class PrimaryWindow : Window
{
    public PrimaryWindow(Page page) : base(page)
    {

    }
}

public class SecondaryWindow : Window
{
    public SecondaryWindow(Page page, string? title)
    {
        ArgumentNullException.ThrowIfNull(page);
        this.Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Value cannot be null or whitespace.", nameof(title)) : title;
    }
}