namespace GiraffeTheLogger.ServiceContract
{
    public interface IQueueService
    {
         void Enqueue(object messageObj, string queueName);
    }
}