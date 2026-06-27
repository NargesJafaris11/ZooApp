using Microsoft.EntityFrameworkCore;
using ZooApp.Models;

namespace ZooApp.Data;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options)
        : base(options)
    {
    }

    public DbSet<Animal> Animals { get; set; }
}