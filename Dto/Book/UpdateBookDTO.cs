using LibFlow.Dto.Link;

namespace LibFlow.Dto.Book;

public class UpdateBookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public AuthorLinkDto Author { get; set; }
}
