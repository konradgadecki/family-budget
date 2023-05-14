using FamilyBudget.Core.Entities;
using FamilyBudget.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyBudget.Infrastructure.DAL.Configurations;

internal class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId)
            .HasConversion(x => x.Value, x => new UserId(x));
    }
}
