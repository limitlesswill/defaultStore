using Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Application.Context
{
    public class User : IdentityUser
    {

    }
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> prodtuct { get; set; }
        public DbSet<Category> category { get; set; }
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }

}
