using LibFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace LibFlow.Data;
public class AppDbContext : DbContext   
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {     
    }

    public DbSet<AuthorModel> Authors { get; set; }
    public DbSet<BookModel> Books { get; set; }
}
