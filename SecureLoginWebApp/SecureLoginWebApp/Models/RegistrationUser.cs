using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecureLoginWebApp.Models
{
	[DataContract]
    public class RegistrationUser
    {
		[DataMember]
        public string Login { get; set; }
 
		[DataMember]
        public string Password { get; set; }

		[DataMember]
        public string RePassword { get; set; }

        public RegistrationUser(string login, string password, string rePassword)
        {
            this.Login = login;
            this.Password = password;
            this.RePassword = rePassword;
        }

        public RegistrationUser()
        {

        }
    }
}
