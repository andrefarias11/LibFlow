using LibFlow.Data;
using LibFlow.Dto.Author;
using LibFlow.Dto.Book;
using LibFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LibFlow.Services.Book;

public class BookService : IBookInterface
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseModel<List<BookModel>>> GetAllBooks()
    {
        ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();
        try
        {
            List<BookModel> books = await _context.Books.ToListAsync();

            response.Data = books;
            response.Message = "Books found";
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            return response;
        }
        return response;
    }
    public async Task<ResponseModel<BookModel>> GetBookById(int idBook)
    {
        ResponseModel<BookModel> resposta = new ResponseModel<BookModel>();
        try
        {

            var book = await _context.Books.Include(a => a.Author)
                .FirstOrDefaultAsync(x => x.Id == idBook);

            if (book is null)
            {
                resposta.Message = "Book not found";
                return resposta;
            }

            resposta.Data = book;
            resposta.Message = "Book found";

            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
    public async Task<ResponseModel<List<BookModel>>> GetBooksByAuthorId(int idAuthor)
    {
        ResponseModel<List<BookModel>> resposta = new ResponseModel<List<BookModel>>();
        try
        {
            var books = await _context.Books
                .Include(a => a.Author)
                .Where(x => x.Author.Id == idAuthor)
                .ToListAsync();

            if (books is null)
            {
                resposta.Message = "Books not found";
                return resposta;
            }

            resposta.Data = books;
            resposta.Message = "Books found";
            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
    public async Task<ResponseModel<List<BookModel>>> InsertBook(CreateBookDTO createBookDTO)
    {
        ResponseModel<List<BookModel>> resposta = new ResponseModel<List<BookModel>>();

        try
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == createBookDTO.Autor.Id);

            if (author is null)
            {
                resposta.Message = "No author record found!";
                return resposta;
            }

            var book = new BookModel()
            {
                Title = createBookDTO.Title,
                Author = author
            };

            _context.Add(book);
            await _context.SaveChangesAsync();

            resposta.Data = await _context.Books.Include(a => a.Author).ToListAsync();

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
    public async Task<ResponseModel<List<BookModel>>> UpdateBook(UpdateBookDTO updateBookDTO)
    {
        ResponseModel<List<BookModel>> resposta = new ResponseModel<List<BookModel>>();
        try
        {

            var book = await _context.Books
                 .Include(a => a.Author)
                 .FirstOrDefaultAsync(x => x.Id == updateBookDTO.Id);

            var author = await _context.Authors
                 .FirstOrDefaultAsync(x => x.Id == updateBookDTO.Autor.Id);

            if (author is null)
            {
                resposta.Message = "Nenhum registro de autor localizado!";
                return resposta;
            }

            if (book is null)
            {
                resposta.Message = "Nenhum registro de livro localizado!";
                return resposta;
            }

            book.Title = updateBookDTO.Titulo;
            book.Author = author;

            _context.Update(book);
            await _context.SaveChangesAsync();

            resposta.Data = await _context.Books.ToListAsync();

            return resposta;
        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
    public async Task<ResponseModel<List<BookModel>>> DeleteBook(int idBook)
    {
        ResponseModel<List<BookModel>> resposta = new ResponseModel<List<BookModel>>();

        try
        {
            var book = await _context.Books.Include(a => a.Author)
                .FirstOrDefaultAsync(x => x.Id == idBook);

            if (book is null)
            {
                resposta.Message = "No book found!";
                return resposta;
            }

            _context.Remove(book);
            await _context.SaveChangesAsync();

            resposta.Data = await _context.Books.ToListAsync();
            resposta.Message = "Book removed successfully!";

            return resposta;

        }
        catch (Exception ex)
        {
            resposta.Message = ex.Message;
            resposta.Status = false;
            return resposta;
        }
    }
}
