using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WorkerHost
{
    [ServiceContract]
    public interface IServerService
    {
        //[OperationContract]
        //string testClass1(string color, string type, string name, string phone);

        [OperationContract]
        String addLostItem(List<string> sColors, string sType, DateTime date, string location, string description,
            int serialNumber, string contactName, string contactPhone, string photoLocation, int key);
        string login(string text1, string text2, bool @checked, string fbToken);
    }
}