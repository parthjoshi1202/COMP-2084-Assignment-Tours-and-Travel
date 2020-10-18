using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using ToursandTravel.Models;

namespace ToursandTravel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //defining model classes here 
        public DbSet<System.Type> Types { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<FinalBill> FinalBills { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //defining relationships 

            //type and package
            builder.Entity<Package>()
                    .HasOne(p => p.Types)
                    .WithMany(c => c.Packages)
                    .HasForeignKey(p => p.TypeId)
                    .HasConstraintName("FK_Packages_TypeId");

            //final bill and package
            builder.Entity<FinalBill>()
                    .HasOne(p => p.Package)
                    .WithMany(c => c.FinalBills)
                    .HasForeignKey(p => p.PackageId)
                    .HasConstraintName("FK_FinalBills_PackageId");

            //shoppingcart and package
            builder.Entity<ShoppingCart>()
                .HasOne(p => p.Package)
                .WithMany(c => c.ShoppingCarts)
                .HasForeignKey(p => p.PackageId)
                .HasConstraintName("FK_ShoppingCarts_PackageId");

            //finalbill and itinerary
            builder.Entity<FinalBill>()
               .HasOne(p => p.Itinerary)
               .WithMany(c => c.FinalBills)
               .HasForeignKey(p => p.ItineraryId)
               .HasConstraintName("FK_FinalBills_ItineraryId");

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
