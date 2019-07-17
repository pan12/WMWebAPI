using BL.Models;
using Microsoft.EntityFrameworkCore;

public class Db : DbContext
{
    
    public DbSet<Room> Rooms {get; set;}
    public DbSet<WaterMeter> WaterMeters { get; set; }
    public DbSet<House> Houses { get; set; }
    public Db(DbContextOptions<Db> option) : base (option)
    {
        Database.EnsureCreated();
    }

}