namespace DevFreela.Core.Services
{
    public interface IMessageBusService
    {
        void Publish(string queue, byte[] message); //As mensagens tem que ser convertidas para bytes que é exatamente o que as filas irão armazenar
    }
}
