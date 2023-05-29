using DevFreela.Core.Services;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusServices : IMessageBusService
    {
        /* OBS:Caso você utilize um servidor externo para fazer a conexão com RabbitMQ, tem que adicionar as informações relativas tipo user e password
        usando IConfiguration*/
        private readonly ConnectionFactory _connectionFactory;
        public MessageBusServices()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                 //Caso utilizar o servidor externo, talvez teria que usar User e Pass
            };
        }
        public void Publish(string queue, byte[] message)
        {
            using (var connection = _connectionFactory.CreateConnection()) //inicia a conexão
            {
                using (var channel = connection.CreateModel()) //cria um canal
                {
                    //Garantir que a fila esteja criada
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false, //fila duravel? Quando reiniciar o svd do RabbitMQ se os dados e metadados da fila ficarão disponiveis (se true)
                        exclusive: false, //Permite apenas uma conexão e após essa conexão acabar, deleta a fila (se true)
                        autoDelete: false, //Permite varias conexões, mas quando todas acabarem é deletado a fila  (se true)
                        arguments: null);

                    //Se a fila não estiver criada, vai ser criada nesse momento
                    channel.BasicPublish(
                        exchange: "", //Responsavel por rotear as mensagens
                        routingKey: queue, //Geralmente a routing key é o nome da fila
                        basicProperties: null,
                        body: message); //Aqui é o corpo da mensagem
                }
            }
        }
    }
}
