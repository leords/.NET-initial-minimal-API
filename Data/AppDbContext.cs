
using Microsoft.EntityFrameworkCore;
using MiniAPI.Models;

namespace MiniAPI.Data
{
    //representação do banco de dados em memória.
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}