using DevFreela.Payments.API.Consumers;
using DevFreela.Payments.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddHostedService<ProcessPaymentConsumer>(); /*AddHostedService  útil quando você tem tarefas ou processos que precisam ser executados continuamente em segundo plano
                                                             enquanto a aplicação estiver em execução, como processar pagamentos, enviar e-mails programados, atualizar caches,
                                                             entre outras atividades. O uso de serviços hospedados facilita a implementação dessas tarefas e garante que elas sejam executadas
                                                             corretamente no contexto do ciclo de vida da aplicação.*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
