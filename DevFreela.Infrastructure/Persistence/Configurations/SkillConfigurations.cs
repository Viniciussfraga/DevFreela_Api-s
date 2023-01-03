﻿using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
