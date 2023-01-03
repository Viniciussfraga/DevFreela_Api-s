using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence {
    public class DevFreelaDbContext : DbContext {
        //Classe que são criadas as tabelas e passadas por EntityFramework
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Inicio Relacionamentos Project
            //Chave primária
            modelBuilder.Entity<Project>()
                .HasKey(p => p.Id);

            //Relacionamento e Chave estrangeira
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Freelancer)
                .WithMany(f => f.FreelanceProjects)
                .HasForeignKey(p => p.IdFreelancer)
                .OnDelete(DeleteBehavior.Restrict); // esse método serve para quando for removida alguma entidade que possui relacionamento
                                                    // Restrict impede que entidades que tenham relacionamentos sejam deletadas 

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany(f => f.OwnedProjects)
                .HasForeignKey(p => p.IdClient)
                .OnDelete(DeleteBehavior.Restrict);
            //Fim Relacionamentos Project

            //Inicio Relacionamentos ProjectComment
            modelBuilder.Entity<ProjectComment>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProjectComment>()
                .HasOne(p => p.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdProject);

            modelBuilder.Entity<ProjectComment>()
               .HasOne(p => p.User)
               .WithMany(p => p.Comments)
               .HasForeignKey(p => p.IdUser);
            //Fim Relacionamentos ProjectComment

            //Inicio Relacionamentos Skill
            modelBuilder.Entity<Skill>()
                .HasKey(p => p.Id);
            //Fim Relacionamentos Skill

            //Inicio Relacionamentos User
            modelBuilder.Entity<User>()
               .HasKey(p => p.Id);

            modelBuilder.Entity<User>()
              .HasMany(u => u.Skills)
              .WithOne()
              .HasForeignKey(u => u.IdSkill)
              .OnDelete(DeleteBehavior.Restrict);
            //Fim Relacionamentos User

            //Inicio Relacionamentos UserSkill
            modelBuilder.Entity<UserSkill>()
               .HasKey(p => p.Id);
            //Fim Relacionamentos Skill
        }

    }
}
