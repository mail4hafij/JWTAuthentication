using System;
using System.Runtime.CompilerServices;

namespace JWTAuthentication
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
