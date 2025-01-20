namespace autoapprove_dashboard2.Data
{
    using autoapprove_dashboard2.Models;  
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Request> Requests { get; set; }
    }
}
