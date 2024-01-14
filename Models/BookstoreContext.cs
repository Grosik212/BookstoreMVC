using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookStore.MVC.Models;

public class BookstoreContext : IdentityDbContext<User>
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<OldAddress> OldAddresses { get; set; }

    public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>().ToTable("books");
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<OrderItem>().ToTable("order_items");
        modelBuilder.Entity<Address>().ToTable("addresses");
        modelBuilder.Entity<OldAddress>().ToTable("old_addresses");
        modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
