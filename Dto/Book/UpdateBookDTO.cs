using LibFlow.Dto.Link;

namespace LibFlow.Dto.Book;

public class UpdateBookDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public AuthorLinkDto Autor { get; set; }
}
