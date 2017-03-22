using System.ServiceModel;

namespace WorkerHost
{
    [ServiceContract]
    public interface IServerService
    {
        [OperationContract]
        string testClass1(string color, string type, string name, string phone);
    }
}