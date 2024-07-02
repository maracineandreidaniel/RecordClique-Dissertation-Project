using Microsoft.EntityFrameworkCore;
using RecordClique_DataAccess.Entities;

namespace RecordClique_DataAccess.Context
{
    public class RecordCliqueContext : DbContext
    {
        public RecordCliqueContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
