using System.Text.Json.Serialization;

namespace Example.Web.API.Models
{
    public class UserVM
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public UserGroupVM UserGroup { get; set; } = new UserGroupVM();
        public UserStateVM UserState { get; set; } = new UserStateVM();
    }
}
