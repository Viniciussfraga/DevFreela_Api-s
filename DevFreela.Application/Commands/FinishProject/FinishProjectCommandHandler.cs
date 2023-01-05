﻿using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject {
    public class FinishProjectCommandHandler 
    {
        private readonly IProjectRepository _projectRepository;
        public FinishProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Finish();

            await _projectRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
