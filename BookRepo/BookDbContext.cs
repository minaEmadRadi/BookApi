using BookRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRepo
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> books {  get; set; }
        public BookDbContext(DbContextOptions<BookDbContext> options)
              : base(options)
        {

        }
    }
}
