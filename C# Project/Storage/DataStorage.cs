namespace C__Project.Models;

// ყველა პროგრამა მონაცემებს ერთ საერთო Data საქაღალდეში ინახავს.
public static class DataStorage
{
    public static string Folder { get; } = FindDataFolder();

    // პროექტიდან გაშვებისას პოულობს პროექტის Data საქაღალდეს.
    // ცალკე გადატანილი პროგრამა იყენებს თავის გვერდით მდებარე Data საქაღალდეს.
    private static string FindDataFolder()
    {
        DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "C# Project.csproj")))
                return Path.Combine(directory.FullName, "Data");

            directory = directory.Parent;
        }

        return Path.Combine(AppContext.BaseDirectory, "Data");
    }
}
