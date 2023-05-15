using DevFreela.Api.Filters;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Payments;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter))) //Filtro de validação, acessando ModelState para fazer as validações
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>()); //AddFluentValidation adiciona todos os validadores no assembly

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

    //Definição de segurança do Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    //
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
         new OpenApiSecurityScheme
         {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
         },
           new string[] {}
         }
    });
});

//Autenticação com JWT, para adicionar o scheme e a configuração do JWT
builder.Services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = builder.Configuration["Jwt:Issuer"],
                      ValidAudience = builder.Configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey
                      (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                  };
              });

//Conexão string com o banco de dados
var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

//Padrão repositorio, injeção de dependência
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

//Injeção de dependência do JWT Authenticator
builder.Services.AddScoped<IAuthService, AuthService>();

//Injeção de dependência PaymentService
builder.Services.AddScoped<IPaymentService, PaymentService>();

//MediatR utiliza o assembly para mapear todos os outros com a interfarce iRequest e iRequestHandler
builder.Services.AddMediatR(typeof(CreateProjectCommand));

//Utilizado para microsserviços
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication(); //Com o UseAuthorization e UseAuthentication tem que ser adicionados para que seja realizada a autorização e autenticação
app.UseAuthorization();

app.MapControllers();

app.Run();
