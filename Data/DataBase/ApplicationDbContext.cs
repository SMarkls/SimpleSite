using Examine.Models;
using Microsoft.EntityFrameworkCore;

namespace Examine.Data.DataBase;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<RegistredUser> Users { get; set; }
}