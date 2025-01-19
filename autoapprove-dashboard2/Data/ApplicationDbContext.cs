namespace autoapprove_dashboard2.Data
{
    using autoapprove_dashboard2.Models;  // Ensure that you are using your models
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        // Constructor for setting up the DbContext with the options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // DbSet representing the 'Requests' table in your database
        public DbSet<Request> Requests { get; set; }
    }
}
