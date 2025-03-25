using LibFlow.Dto.Author;
using LibFlow.Models;

namespace LibFlow.Services.Author;

public interface IAuthorInterface
{
    Task<ResponseModel<List<AuthorModel>>> GetAllAuthors();
    Task<ResponseModel<AuthorModel>> GetAuthorById(int idAuthor);
    Task<ResponseModel<AuthorModel>> GetAuthorByBookId (int idBook);
    Task<ResponseModel<List<AuthorModel>>> InsertAuthor(CreateAuthorDTO createAuthorDTO);
    Task<ResponseModel<List<AuthorModel>>> UpdateAuthor(UpdateAuthorDTO updateAuthorDTO);
    Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int idAutor);
}
