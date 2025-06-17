using CRUD_API_Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API_Demo.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Book> Books { get; set; }
    }
}
