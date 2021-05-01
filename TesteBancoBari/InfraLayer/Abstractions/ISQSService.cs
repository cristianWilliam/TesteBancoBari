namespace TesteBancoBari.InfraLayer.Abstractions
{
    public interface ISQSService<T> : IQueueService<T> where T : class
    {
    }
}
