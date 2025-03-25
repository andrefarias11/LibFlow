﻿using Azure;
using LibFlow.Data;
using LibFlow.Dto.Author;
using LibFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LibFlow.Services.Author;

public class AuthorService : IAuthorInterface
{
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<AuthorModel>>> GetAllAuthors()
    {
        ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();
        try
        {
            List<AuthorModel> authors = await _context.Authors.ToListAsync();

            response.Data = authors;
            response.Message = "Authors found";
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            return response;
        }
        return response;
    }

    public async Task<ResponseModel<AuthorModel>> GetAuthorByBookId(int bookId)
    {
        ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();
        try
        {
            var author = await _context.Books
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(x => x.Id == bookId);

            if (author is null)
            {
                response.Status = false;
                response.Message = "Book not found";
                return response;
            }

            response.Data = author.Author;
            response.Message = "Author found";

            return response;
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<AuthorModel>> GetAuthorById(int authorId)
    {
        ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();
        try
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorId);

            if (author is null)
            {
                response.Status = false;
                response.Message = "Author not found";
                return response;
            }

            response.Data = author;
            response.Message = "Author found";
            return response;
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<List<AuthorModel>>> InsertAuthor(CreateAuthorDTO createAuthorDTO)
    {
        ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();
        try
        {
            var author = new AuthorModel()
            {
                Name = createAuthorDTO.Name,
                Surname = createAuthorDTO.Surname
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            response.Data = await _context.Authors.ToListAsync();
            response.Message = "Author inserted";

            return response;
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ResponseModel<List<AuthorModel>>> UpdateAuthor(UpdateAuthorDTO updateAuthorDTO)
    {
        ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

        try
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == updateAuthorDTO.Id);

            if (author is null)
            {
                response.Message = "Nenhum autor localizado!";
                return response;
            }
            ;

            author.Name = updateAuthorDTO.Name;
            author.Surname = updateAuthorDTO.Surname;

            _context.Update(author);
            await _context.SaveChangesAsync();

            response.Data = await _context.Authors.ToListAsync();
            response.Message = "Author updated";

            return response;
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            return response;
        }

    }

    public async Task<ResponseModel<List<AuthorModel>>> DeleteAuthor(int authorId)
    {
        ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

        try
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == authorId);

            if (author is null)
            {
                response.Message = "Author not found";
                return response;
            };
            _context.Authors.Remove(author);
            _context.SaveChanges();

            response.Data = await _context.Authors.ToListAsync();
            response.Message = "Author deleted";
            return response;
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            return response;
        }

    }
}
