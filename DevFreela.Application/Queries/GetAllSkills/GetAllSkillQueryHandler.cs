using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills {
    public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillQuery, List<SkillViewModel>> 
    {
        private readonly string _connectionString;
        public GetAllSkillQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<List<SkillViewModel>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
        {
            //Utilizando Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                var skill = await sqlConnection.QueryAsync<SkillViewModel>(script);

                return skill.ToList();
            }

            // Utilizando EF Core \/
            //var skill = _dbContext.Skills;
            //var skillsViewModel = skill.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();

            //return skillsViewModel;
        }
    }
}
