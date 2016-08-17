using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ecom.API.Contexts;

namespace Ecom.API.Migrations
{
    [DbContext(typeof(BaseContext))]
    [Migration("20160813102204_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ecom.API.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2016, 8, 13, 10, 22, 4, 366, DateTimeKind.Utc));

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0.0m);

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<string>("Supplier")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Products");
                });
        }
    }
}
