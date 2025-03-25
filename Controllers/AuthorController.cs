using LibFlow.Dto.Author;
using LibFlow.Models;
using LibFlow.Services.Author;
using Microsoft.AspNetCore.Mvc;

namespace LibFlow.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorInterface _authorService;

    public AuthorController(IAuthorInterface authorService)
    {
        _authorService = authorService;
    }

    // GET api/authors
    [HttpGet]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> GetAll()
    {
        var authors = await _authorService.GetAllAuthors();
        return Ok(authors);
    }

    // GET api/authors/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<AuthorModel>>> GetById(int id)
    {
        var author = await _authorService.GetAuthorById(id);
        return Ok(author);
    }

    // GET api/authors/{bookId}
    [HttpGet("by-author/{bookId}")]
    public async Task<ActionResult<ResponseModel<AuthorModel>>> GetByBookId(int bookId)
    {
        var author = await _authorService.GetAuthorByBookId(bookId);
        return Ok(author);
    }

    // POST api/authors
    [HttpPost]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> Create([FromBody] CreateAuthorDTO createAuthorDTO)
    {
        var author = await _authorService.InsertAuthor(createAuthorDTO);
        return Ok(author);
    }

    // PUT api/authors/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> Update(int id, [FromBody] UpdateAuthorDTO updateAuthorDTO)
    {
        var author = await _authorService.UpdateAuthor(updateAuthorDTO);
        return Ok(author);
    }

    // DELETE api/authors/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> Delete(int id)
    {
        var author = await _authorService.DeleteAuthor(id);
        return Ok(author);
    }
}
