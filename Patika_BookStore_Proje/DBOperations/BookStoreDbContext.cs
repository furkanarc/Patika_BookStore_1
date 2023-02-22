
using Microsoft.EntityFrameworkCore;

namespace Patika_BookStore_Proje.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {}
    public DbSet<Book> Books { get; set; }
    }
}