using Microsoft.EntityFrameworkCore;
using webProje.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace webProje.Utility
{
    //veri tabanında ef tablo oluşturması için ilgili model sınıflarınızı DbContexte eklemeliyiz
    public class AppDBContext : IdentityDbContext
    {
        public DbSet<BookType> BookTypes { get; set; } //EF içinde bulunana booktype classının DB de karşılığı olacak olan eleman Booktyps
        public DbSet<Book>  Books { get; set; }

        public DbSet<Rent> Rents { get; set; }


        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options) { 
        
        }
    }
}
