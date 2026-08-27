using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApi.Domain.Entities;

namespace TestApi.Infra.Sql.Configurations
{
    public class TestSqlConfiguration : IEntityTypeConfiguration<TestSql>
    {
        public void Configure(EntityTypeBuilder<TestSql> builder)
        {

            builder.Ignore(entity => entity.Notifications);
            builder.Ignore(entity => entity.Valid);
            builder.Ignore(entity => entity.Invalid);

            builder.HasKey(x => x.Id);

            builder
                .Property(t => t.TestString)
                .IsRequired();

           builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

            builder.ToTable("TestSql");
        }
    }
}
