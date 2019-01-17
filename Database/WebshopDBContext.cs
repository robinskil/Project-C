using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using ProjectC_v2.Models;
using ProjectC_v2.Models.ViewModels;

namespace ProjectC_v2.Database
{
    public class WebshopDbContext : DbContext
    {
        //Holds our connection string
        public WebshopDbContext(DbContextOptions<WebshopDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<GamePlatform> GamePlatform { get; set; }
        public virtual DbSet<GameType> GameType { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<PGRating> PGRating { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductFeature> ProductFeature { get; set; }
        public virtual DbSet<ProductVideo> ProductVideo { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<TransactionInfo> TransactionInfo {get;set;}
        public virtual DbSet<WishList> WishList {get;set;}
        public virtual DbSet<WishListItem> WishListItem {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(oi => new {oi.InventoryId, oi.OrderId});
            modelBuilder.Entity<ShoppingCartItem>().HasKey(oi => new { oi.InventoryId, oi.ShoppingCartId });
            modelBuilder.Entity<WishListItem>().HasKey(oi => new { oi.InventoryId, oi.WishListId });
        }


        public DbSet<ProjectC_v2.Models.Publisher> Publisher { get; set; }


        public DbSet<ProjectC_v2.Models.ViewModels.OrderTransactionViewModel> OrderTransactionViewModel { get; set; }


        public DbSet<ProjectC_v2.Models.ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}