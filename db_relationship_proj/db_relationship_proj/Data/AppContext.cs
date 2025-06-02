using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using db_relationship_proj.Models;

namespace db_relationship_proj.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { modelBuilder.Entity<ItemClient>()
                .HasKey(ic => new {ic.ClientId,ic.ItemId});

            modelBuilder.Entity<ItemClient>().
                HasOne(i => i.item).
                WithMany(ic=>ic.ItemClients).
                HasForeignKey(i=>i.ItemId);


            modelBuilder.Entity<ItemClient>().
                HasOne(c => c.client).
                WithMany(ic => ic.itemClients).
                HasForeignKey(i => i.ClientId);


            modelBuilder.Entity<SerialNumber>()
         .HasOne(s => s.item)
         .WithMany() // or WithOne() if strictly one-to-one
         .HasForeignKey(s => s.ItemId)
         .OnDelete(DeleteBehavior.SetNull);



            modelBuilder.Entity<Item>().HasData(new Item() { Id = 10, Name = "microphone", Price = 245, SerialNumberId = 10 });

            modelBuilder.Entity<SerialNumber>().HasData(new SerialNumber() { Id = 10, Name = "mic10", ItemId = 10 });
            modelBuilder.Entity<Category>().HasData(new Category() { Id = 10, Name = "Electronics" });
            modelBuilder.Entity<Category>().HasData(new Category() { Id = 11, Name = "Books" });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemClient> ItemClients { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
