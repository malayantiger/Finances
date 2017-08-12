using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Finances.Flat.Infrastructure.Database
{
    public class FlatContext : DbContext
    {
        public FlatContext(DbContextOptions<FlatContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>().HasKey(e => e.Id);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Expense>()
                .Property(e => e.Cost)
                .ForSqliteHasColumnType("INTEGER");

            modelBuilder.Entity<ExpenseMetatags>()
                .HasKey(em => new { em.ExpenseId, em.MetatagId });

            modelBuilder.Entity<Expense>()
                .Property(e => e.Created)
                .ForSqliteHasColumnType("DATETIME");

            modelBuilder.Entity<ExpenseMetatags>()
                .HasOne(em => em.Expense)
                .WithMany(e => e.ExpenseMetatags)
                .HasForeignKey(em => em.ExpenseId);

            modelBuilder.Entity<ExpenseMetatags>()
                .HasOne(em => em.Metatag)
                .WithMany(m => m.ExpenseMetatags)
                .HasForeignKey(em => em.MetatagId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Metatag> Metatags { get; set; }
    }

    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public ICollection<ExpenseMetatags> ExpenseMetatags { get; set; }
        public DateTime Created { get; set; }
    }

    public class ExpenseMetatags
    {
        public int ExpenseId { get; set; }
        public Expense Expense { get; set; }

        public int MetatagId { get; set; }
        public Metatag Metatag { get; set; }
    }

    public class Metatag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ICollection<ExpenseMetatags> ExpenseMetatags { get; set; }
    }
}
