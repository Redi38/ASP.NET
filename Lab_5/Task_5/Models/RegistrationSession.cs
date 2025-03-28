using Newtonsoft.Json;

namespace Task_5.Models
{
    public class RegistrationSession
    {
        private const string SessionKey = "RegistrationData";

        public required string Address { get; set; }
        public required string Password { get; set; }
        public required string Login { get; set; }
        public required string Email { get; set; }

        public RegistrationSession(string address, string password, string login, string email)
        {
            Address = address;
            Password = password;
            Login = login;
            Email = email;
        }

        public static RegistrationSession GetSessionData(ISession session)
        {
            var data = session.GetString(SessionKey);
            if (string.IsNullOrEmpty(data))
            {
                throw new InvalidOperationException("No session data found.");
            }

            var sessionData = JsonConvert.DeserializeObject<RegistrationSession>(data);
            if (sessionData == null)
            {
                throw new InvalidOperationException("Deserialized session data is null.");
            }

            return sessionData;
        }

        public void SaveSessionData(ISession session)
        {
            session.SetString(SessionKey, JsonConvert.SerializeObject(this));
        }
    }
}