using Dapper;
using DevFreela.Core.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Core.Repositories {
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;
        public SkillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }
        public async Task<List<SkillDTO>> GetAllAsync()
        {
            //Utilizando Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                var skill = await sqlConnection.QueryAsync<SkillDTO>(script);

                return skill.ToList();
            }
            // Utilizando EF Core \/
            //var skill = _dbContext.Skills;
            //var skillsViewModel = skill.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();

            //return skillsViewModel;
        }
    }
}
