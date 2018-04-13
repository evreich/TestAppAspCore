using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;
using static TestAppAspCore.Models.BookOrder;

namespace TestAppAspCore.EFCore
{
    public class BooksContext : IdentityDbContext<User, UserRole, string>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<MenuElement> MenuElements { get; set; }
        public DbSet<UserRoleMenuElement> UserRoleMenuElements { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many-to-many для книг и заказов
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

            //many-to-many для ролей и связанных пунктов меню
            modelBuilder.Entity<UserRoleMenuElement>()
                .HasKey(user_menu => new { user_menu.UserRoleId, user_menu.MenuElementId });

            modelBuilder.Entity<UserRoleMenuElement>()
                .HasOne(user_menu => user_menu.MenuElement)
                .WithMany(b => b.UserRoleMenuElements)
                .HasForeignKey(user_menu => user_menu.MenuElementId);

            modelBuilder.Entity<UserRoleMenuElement>()
                .HasOne(user_menu => user_menu.UserRole)
                .WithMany(c => c.UserRoleMenuElements)
                .HasForeignKey(user_menu => user_menu.UserRoleId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
