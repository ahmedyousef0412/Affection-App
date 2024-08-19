
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Affection.Infrastructure.EntitiesConfiguration;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

      

        // Date of Birth: Required
        builder.Property(u => u.DateOfBirth)
            .IsRequired();

        // Known As: Required with max length
        builder.Property(u => u.KnowAs)
            .IsRequired()
            .HasMaxLength(50);

        // Created and LastActive: Required, set default value
        builder.Property(u => u.Created)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); // Use SQL Server's GETDATE function

        builder.Property(u => u.LastActive)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        // Gender: Required, stored as a string (if needed)
        builder.Property(u => u.Gender)
            .IsRequired()
            .HasConversion<string>(); // Converts enum to string

        // Introduction, LookingFor, Interests: Optional
        builder.Property(u => u.Introduction)
            .HasMaxLength(500);

        builder.Property(u => u.LookingFor)
            .HasMaxLength(500);

        builder.Property(u => u.Interests) // Assuming 'Intrestes' should be 'Interests'
            .HasMaxLength(500);

        // City and Country: Required with max length
        builder.Property(u => u.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Country)
            .IsRequired()
            .HasMaxLength(100);

        // IsRestricted: Default to false
        builder.Property(u => u.IsRestricted)
            .IsRequired()
            .HasDefaultValue(false);

        // Photos: Configure one-to-many relationship
        builder.HasMany(u => u.Photos)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete when user is deleted


        //Refresh Token

        builder.OwnsMany(u => u.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");
    }
}
