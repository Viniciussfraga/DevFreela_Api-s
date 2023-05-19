using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payments
{
    //Classe que faz a chamada do microsserviço
    public class PaymentService : IPaymentService
    {
        

        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }
        public void ProcessPayment(PaymentInfoDTO paymentoInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentoInfoDTO); //transformando em string JSON
            
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson); // transformando a string JSON em bytes

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);

        }

        /*private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentsBaseUrl; //Tbm não é mais necessário essa dependência
        public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        }*/

        /*public void ProcessPayment(PaymentInfoDTO paymentoInfoDTO)
        {
            // OBS: O metódo comentado foi refatorado, antes a comunicação era HTTP

            var url = $"{_paymentsBaseUrl}/api/payments";
            var paymentInfoJson = JsonSerializer.Serialize(paymentoInfoDTO); //transformando em string JSON

            var paymentInfoContent = new StringContent(
                paymentInfoJson, //dados json
                Encoding.UTF8, //Encoding utilzado para as requisições
                "application/json" //tipo do corpo da requisição
                );

            var httpClient = _httpClientFactory.CreateClient("Payments"); //criando uma instância para fazer a requisição http *Entre () é um apelido pra a requisição*

            var response = await httpClient.PostAsync(url, paymentInfoContent); //efetuando requisição

            return response.IsSuccessStatusCode;
        }*/
    }
}
