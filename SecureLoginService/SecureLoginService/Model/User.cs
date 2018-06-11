using System.Runtime.Serialization;

namespace SecureLoginService.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        
        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
    }
}
