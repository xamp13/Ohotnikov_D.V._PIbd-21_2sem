using FlowerShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopDatabaseImplement
{
    public class FlowerShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=HOME-PC;Initial Catalog=FlowerShopDatabaseH;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Flower> Flowers { set; get; }
        public virtual DbSet<Bouquet> Bouquets { set; get; }
        public virtual DbSet<BouquetFlower> BouquetFlowers { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<StorageFlower> StorageFlowers { set; get; }
        public virtual DbSet<Storage> Storages { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Implementer> Implementers { set; get; }
        public virtual DbSet<MessageInfo> MessageInfoes { set; get; }
    }
}