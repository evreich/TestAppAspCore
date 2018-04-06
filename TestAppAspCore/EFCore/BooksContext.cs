using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;
using static TestAppAspCore.Models.BookOrder;

namespace TestAppAspCore.EFCore
{
    public class BooksContext : IdentityDbContext<User>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookOrder> BookOrder { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            modelBuilder.Entity<BookOrder>()
                .HasKey(bc => new { bc.BookId, bc.OrderId });

            modelBuilder.Entity<BookOrder>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookOrders)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookOrder>()
                .HasOne(bc => bc.Order)
                .WithMany(c => c.BookOrders)
                .HasForeignKey(bc => bc.OrderId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
