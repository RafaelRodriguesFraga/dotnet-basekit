using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Domain.Entities;

namespace TestApi.Infra.Sql.Configurations
{
    public class TestSqlConfiguration : IEntityTypeConfiguration<TestSql>
    {
        public void Configure(EntityTypeBuilder<TestSql> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("varchar(36)");

            builder
                .Property(t => t.TestString)
                .IsRequired();

            builder
                .Property(t => t.CreatedAt)
                .HasColumnType("datetime");

            builder.ToTable("TestSql");
        }
    }
}
