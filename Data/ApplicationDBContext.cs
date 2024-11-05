using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> Roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            };

            builder.Entity<IdentityRole>().HasData(Roles);

            builder.Entity<Portfolio>(portfolioBuilder =>
                portfolioBuilder.HasKey(key => new { key.AppUserId, key.StockId })
            );

            builder.Entity<Portfolio>()
                .HasOne(portfolio => portfolio.AppUser)
                .WithMany(user => user.Portfolios)
                .HasForeignKey(fk => fk.AppUserId);

            builder.Entity<Portfolio>()
                .HasOne(portfolio => portfolio.Stock)
                .WithMany(stock => stock.Portfolios)
                .HasForeignKey(fk => fk.StockId);
        }
    }
}