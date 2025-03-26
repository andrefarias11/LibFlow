using LibFlow.Data;
using LibFlow.Dto.Author;
using LibFlow.Dto.Book;
using LibFlow.Models;
using LibFlow.Resorces.Author;
using LibFlow.Resorces.Book;
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
            response.Message = BookMsg.DISP0001;
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
                resposta.Message = BookMsg.DISP0002;
                return resposta;
            }

            resposta.Data = book;
            resposta.Message = BookMsg.DISP0003;

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
                resposta.Message = BookMsg.DISP0002;
                return resposta;
            }

            resposta.Data = books;
            resposta.Message = BookMsg.DISP0001;
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
                .FirstOrDefaultAsync(x => x.Id == createBookDTO.Author.Id);

            if (author is null)
            {
                resposta.Message = BookMsg.DISP0004;
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
                 .FirstOrDefaultAsync(x => x.Id == updateBookDTO.Author.Id);

            if (author is null)
            {
                resposta.Message = AuthorMsg.DISP0004;
                return resposta;
            }

            if (book is null)
            {
                resposta.Message = BookMsg.DISP0002;
                return resposta;
            }

            book.Title = updateBookDTO.Title;
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
                resposta.Message = BookMsg.DISP0002;
                return resposta;
            }

            _context.Remove(book);
            await _context.SaveChangesAsync();

            resposta.Data = await _context.Books.ToListAsync();
            resposta.Message = BookMsg.DISP0005;

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
