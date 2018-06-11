using System.Runtime.Serialization;

namespace SecureLoginService.Model
{
    [DataContract]
    public class Response
    {
        [DataMember]
        public Types Type { get; set; }

        [DataMember]
        public string Message { get; set; }

        public Response(Types type, string message = "none")
        {
            Type = type;
            Message = message;
        }
    }

    [DataContract]
    public enum Types
    {
        [EnumMember]
        CorrectLogin,
        [EnumMember]
        IncorrectLogin,
        [EnumMember]
        AccountExist,
        [EnumMember]
        AccountCreated,
        [EnumMember]
        Error
    }
}
