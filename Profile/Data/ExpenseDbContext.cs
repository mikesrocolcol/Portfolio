using Profile.Models;
using Microsoft.EntityFrameworkCore;

namespace Profile.Data
{
    public class ExpenseDbContext : DbContext

    {

        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {

        }
        public DbSet<ExpenseEntity> Categories { get; set; }
        public DbSet<ExpenseCategoryEntity> ExpenseCategory { get; set; }
        public DbSet<ImageEntity> ImgCategories { get; set; }
     

    }
}
