using System.Text.Json.Serialization;

namespace LibFlow.Models;

public class AuthorModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<BookModel> Books { get; set; }

}
