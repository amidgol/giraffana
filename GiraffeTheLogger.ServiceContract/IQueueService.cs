namespace GiraffeTheLogger.ServiceContract
{
    public interface IQueueService
    {
         void Enqueue(string message, string queueName);
    }
}