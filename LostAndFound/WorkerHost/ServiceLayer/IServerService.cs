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
            int serialNumber, string companyName, string contactName, string contactPhone, string photoLocation, string token, int key);
    }
}