using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{

    public string Nick { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Dodaj dowolne konfiguracje właściwości dla User
        // Na przykład, ustawienie maksymalnej długości dla FirstName i LastName:
        builder.Property(x => x.FirstName).HasMaxLength(255);
        builder.Property(x => x.LastName).HasMaxLength(255);
    }
}