using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using LibraryManagementSystem.Server.Models.MyLibraryDB;

namespace LibraryManagementSystem.Server.Data
{
    public partial class MyLibraryDBContext : DbContext
    {
        public MyLibraryDBContext()
        {
        }

        public MyLibraryDBContext(DbContextOptions<MyLibraryDBContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>()
              .HasOne(i => i.BindingDetail)
              .WithMany(i => i.BookDetails)
              .HasForeignKey(i => i.BindingID)
              .HasPrincipalKey(i => i.BindingID);

            builder.Entity<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>()
              .HasOne(i => i.CategoryDetail)
              .WithMany(i => i.BookDetails)
              .HasForeignKey(i => i.CategoryID)
              .HasPrincipalKey(i => i.CategoryID);

            builder.Entity<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail>()
              .HasOne(i => i.BookShelf)
              .WithMany(i => i.BookDetails)
              .HasForeignKey(i => i.ShelfID)
              .HasPrincipalKey(i => i.ShelfID);

            builder.Entity<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>()
              .HasOne(i => i.BookDetail)
              .WithMany(i => i.BorrowerDetails)
              .HasForeignKey(i => i.BookID)
              .HasPrincipalKey(i => i.BookID);

            builder.Entity<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>()
              .HasOne(i => i.LibraryClient)
              .WithMany(i => i.BorrowerDetails)
              .HasForeignKey(i => i.BorrowedBy)
              .HasPrincipalKey(i => i.LibraryClientID);

            builder.Entity<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail>()
              .HasOne(i => i.LibraryEmployee)
              .WithMany(i => i.BorrowerDetails)
              .HasForeignKey(i => i.IssuedBy)
              .HasPrincipalKey(i => i.LibraryEmployeeID);
            this.OnModelBuilding(builder);
        }

        public DbSet<LibraryManagementSystem.Server.Models.MyLibraryDB.BindingDetail> BindingDetails { get; set; }

        public DbSet<LibraryManagementSystem.Server.Models.MyLibraryDB.BookDetail> BookDetails { get; set; }

        public DbSet<LibraryManagementSystem.Server.Models.MyLibraryDB.BookShelf> BookShelves { get; set; }

        public DbSet<LibraryManagementSystem.Server.Models.MyLibraryDB.BorrowerDetail> BorrowerDetails { get; set; }

        public DbSet<LibraryManagementSystem.Server.Models.MyLibraryDB.CategoryDetail> CategoryDetails { get; set; }

        public DbSet<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryClient> LibraryClients { get; set; }

        public DbSet<LibraryManagementSystem.Server.Models.MyLibraryDB.LibraryEmployee> LibraryEmployees { get; set; }
    }
}