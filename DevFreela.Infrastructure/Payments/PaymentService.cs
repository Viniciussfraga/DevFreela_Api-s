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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentsBaseUrl;
       public PaymentService( IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        }

        public async Task<bool> ProcessPayment(PaymentInfoDTO paymentoInfoDTO)
        {
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
        }
    }
}
