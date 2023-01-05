using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations {
    public class UserConfigurations : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Inicio Relacionamentos User
            builder.HasKey(p => p.Id);

           builder.HasMany(u => u.Skills)
              .WithOne()
              .HasForeignKey(u => u.IdSkill)
              .OnDelete(DeleteBehavior.Restrict);
            //Fim Relacionamentos User
        }
    }
}
