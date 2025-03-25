using LibFlow.Dto.Author;
using LibFlow.Dto.Book;
using LibFlow.Models;

namespace LibFlow.Services.Book;

public interface IBookInterface
{
    Task<ResponseModel<List<BookModel>>> GetAllBooks();
    Task<ResponseModel<BookModel>> GetBookById(int idBook);
    Task<ResponseModel<List<BookModel>>> GetBooksByAuthorId(int idAuthor);
    Task<ResponseModel<List<BookModel>>> InsertBook(CreateBookDTO createBookDTO);
    Task<ResponseModel<List<BookModel>>> UpdateBook(UpdateBookDTO updateBookDTO);
    Task<ResponseModel<List<BookModel>>> DeleteBook(int idBook);
}
