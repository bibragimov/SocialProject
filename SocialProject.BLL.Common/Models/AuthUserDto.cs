using Newtonsoft.Json;

namespace SocialProject.BLL.Common.Models
{
    public class AuthUserDto
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "rememberMe")]
        public bool IsRememberMe { get; set; }
    }
}