using LibFlow.Dto.Book;
using LibFlow.Models;
using LibFlow.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibFlow.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface _bookService;

        public BookController(IBookInterface bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetAll()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{idBook}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> GetBookById(int idBook)
        {
            var book = await _bookService.GetBookById(idBook);
            return Ok(book);
        }

        [HttpGet("by-author/{authorId}")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetBooksByAuthorId(int authorId)
        {
            var books = await _bookService.GetBooksByAuthorId(authorId);
            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> Create([FromBody] CreateBookDTO createBookDTO)
        {
            var book = await _bookService.InsertBook(createBookDTO);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> Update(int id, [FromBody] UpdateBookDTO updateBookDTO)
        {
            var book = await _bookService.UpdateBook(updateBookDTO);
            return Ok(book);
        }

        [HttpDelete("{idBook}")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> Delete(int idBook)
        {
            var book = await _bookService.DeleteBook(idBook);
            return Ok(book);
        }
    }
}
