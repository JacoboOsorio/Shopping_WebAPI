using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.DAL.Entities;

namespace ShoppingWebAPI.DAL
{
    //Here's where i manipulate the database
    //for me to do whatever i wanna do
    public class DataBaseContext : DbContext
    {
        //Takes an instance from "DbContextOptions" pre-defined
        //on "EntityFrameworkCore" library, and i will add it a
        //property called "options" to control the database
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        //Naming the table for the database
        public DbSet<Country> Countries { get; set; }

        //Creating an index to control duplicated data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
        }
    }
}
