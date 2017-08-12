using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Finances.Flat.Infrastructure.Database;

namespace Finances.Flat.Migrations
{
    [DbContext(typeof(FlatContext))]
    partial class FlatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Finances.Flat.Infrastructure.Database.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cost")
                        .HasAnnotation("Sqlite:ColumnType", "INTEGER");

                    b.Property<DateTime>("Created")
                        .HasAnnotation("Sqlite:ColumnType", "DATETIME");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Finances.Flat.Infrastructure.Database.ExpenseMetatags", b =>
                {
                    b.Property<int>("ExpenseId");

                    b.Property<int>("MetatagId");

                    b.HasKey("ExpenseId", "MetatagId");

                    b.HasIndex("MetatagId");

                    b.ToTable("ExpenseMetatags");
                });

            modelBuilder.Entity("Finances.Flat.Infrastructure.Database.Metatag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Metatags");
                });

            modelBuilder.Entity("Finances.Flat.Infrastructure.Database.ExpenseMetatags", b =>
                {
                    b.HasOne("Finances.Flat.Infrastructure.Database.Expense", "Expense")
                        .WithMany("ExpenseMetatags")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Finances.Flat.Infrastructure.Database.Metatag", "Metatag")
                        .WithMany("ExpenseMetatags")
                        .HasForeignKey("MetatagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
