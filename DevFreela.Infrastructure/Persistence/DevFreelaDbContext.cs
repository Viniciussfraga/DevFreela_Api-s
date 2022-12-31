using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core 1", "Descricao de Projeto 1", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core 2", "Descricao de Projeto 2", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core 2", "Descricao de Projeto 3", 1, 1, 30000),
            };
            Users = new List<User>
            {
                new User("Vinicius Fraga", "viniciusfrg@outlook.com.br", new DateTime(2001, 7, 7)),
                new User("Fabio Tila", "fabiotila@outlook.com.br", new DateTime(2002, 4, 9)),
                new User("Zé Andrade", "zeandrade@outlook.com.br", new DateTime(1975, 2, 9)),
            };

            Skills = new List<Skill>
            {
                new Skill(".Net Core"),
                new Skill("C#"),
                new Skill("SQL")

            };
        }
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }

    }
}
