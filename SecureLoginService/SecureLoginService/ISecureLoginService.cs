using SecureLoginService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SecureLoginService
{
    [ServiceContract]
    public interface ISecureLoginService
    {
        [OperationContract]
        Response Login(User user);

        [OperationContract]
        Response AddUser(User user);
        
    }
}
