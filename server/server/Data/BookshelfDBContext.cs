using Microsoft.EntityFrameworkCore;
using server.Model;

namespace server.Data
{
    public class BookshelfDBContext : DbContext
    {
        public BookshelfDBContext(DbContextOptions<BookshelfDBContext> options): base(options) { }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
