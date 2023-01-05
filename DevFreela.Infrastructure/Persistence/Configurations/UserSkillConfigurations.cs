using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations {
    public class UserSkillConfigurations : IEntityTypeConfiguration<UserSkill> {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {

            //Inicio Relacionamentos UserSkill
          builder.HasKey(p => p.Id);
            //Fim Relacionamentos Skill
        }
    }
}
