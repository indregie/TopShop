using Microsoft.EntityFrameworkCore;
using Domain.Data.Entities;

namespace TopShop.Data;

public class DataContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    public DataContext(DbContextOptions<DataContext>
        options) : base(options)
    {

    }
}
