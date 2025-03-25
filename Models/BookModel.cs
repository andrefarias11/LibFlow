namespace LibFlow.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public AuthorModel Author { get; set; }
}
