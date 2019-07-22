using BL.Models;
using Microsoft.EntityFrameworkCore;

public class Db : DbContext
{
    
    public DbSet<Room> Rooms {get; set;}
    public DbSet<WaterMeter> WaterMeters { get; set; }
    public virtual DbSet<House> Houses { get; set; }

    public Db()
    {

    }
    public Db(DbContextOptions<Db> option) : base (option)
    {
        Database.EnsureCreated();
    }
}