using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations {
    public class SkillConfigurations : IEntityTypeConfiguration<Skill> {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            //Inicio Relacionamentos Skill
            builder.HasKey(p => p.Id);
            //Fim Relacionamentos Skill
        }
    }
}
