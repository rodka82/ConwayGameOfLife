using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConwayGameOfLife.Infra.Data.Mapping
{
    public class BoardMapping : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.ToTable("Board");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.State)
                .IsRequired();
            builder.Ignore(x => x.Grid);
            builder.Ignore(x => x.Rows);
            builder.Ignore(x => x.Cols);

        }
    }
}
