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

    [HttpGet]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> GetAll()
    {
        var authors = await _authorService.GetAllAuthors();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<AuthorModel>>> GetById(int id)
    {
        var author = await _authorService.GetAuthorById(id);
        return Ok(author);
    }

    [HttpGet("by-author/{bookId}")]
    public async Task<ActionResult<ResponseModel<AuthorModel>>> GetByBookId(int bookId)
    {
        var author = await _authorService.GetAuthorByBookId(bookId);
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> Create([FromBody] CreateAuthorDTO createAuthorDTO)
    {
        var author = await _authorService.InsertAuthor(createAuthorDTO);
        return Ok(author);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> Update(int id, [FromBody] UpdateAuthorDTO updateAuthorDTO)
    {
        var author = await _authorService.UpdateAuthor(updateAuthorDTO);
        return Ok(author);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> Delete(int id)
    {
        var author = await _authorService.DeleteAuthor(id);
        return Ok(author);
    }
}
