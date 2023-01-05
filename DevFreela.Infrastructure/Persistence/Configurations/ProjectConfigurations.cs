using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations {
    public class ProjectConfigurations : IEntityTypeConfiguration<Project> {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            //Inicio Relacionamentos Project
            //Chave primária
            builder.HasKey(p => p.Id);

            //Relacionamento e Chave estrangeira
           builder.HasOne(p => p.Freelancer)
                .WithMany(f => f.FreelanceProjects)
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict); // esse método serve para quando for removida alguma entidade que possui relacionamento
                                                    // Restrict impede que entidades que tenham relacionamentos sejam deletadas 

            builder.HasOne(p => p.Client)
                .WithMany(f => f.OwnedProjects)
                .HasForeignKey(p => p.IdClient)
                .OnDelete(DeleteBehavior.Restrict);
            //Fim Relacionamentos Project
        }
    }
}
