using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace JWTAuthentication.Model
{
    public class SignInManager
    {
        private readonly IConfiguration _config;

        public SignInManager(IConfiguration config)
        {
            _config = config;
        }

        public async Task<SignInResult> SignIn(string email, string password)
        {
            // Do the email verification here.
            // Assuming the the email verification is done and we 
            // have the following loggedInUser.
            UserModel loggedInUser = new UserModel
            {
                Email = "test@test.com",
                Role = RoleModel.Client,
                UserId = 123456789
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Issuer"],
                GetClaims(loggedInUser),
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new SignInResult(loggedInUser, true, new JwtSecurityTokenHandler().WriteToken(token));
        }
        

        private IEnumerable<Claim> GetClaims(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, "" + user.UserId),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
            };
            return claims;
        }
    }
}
