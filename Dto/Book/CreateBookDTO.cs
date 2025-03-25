using LibFlow.Dto.Link;

namespace LibFlow.Dto.Book;

public class CreateBookDTO
{
    public string Title { get; set; } = string.Empty;
    public AuthorLinkDto Autor { get; set; } 
}
