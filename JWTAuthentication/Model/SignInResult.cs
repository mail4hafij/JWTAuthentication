

namespace JWTAuthentication.Model
{
    public class SignInResult
    {
        public SignInResult(bool success, string error = null)
        {
            Success = success;
            Error = error;
        }

        public SignInResult(UserModel user, bool success, string jwt = null)
        {
            User = user;
            Success = success;
            JWT = jwt;
        }

        public UserModel User{ get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
        public string JWT { get; set; }
    }
}
