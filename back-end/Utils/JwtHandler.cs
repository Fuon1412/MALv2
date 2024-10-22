using Microsoft.IdentityModel.Tokens;
using Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace back_end.Utils
{
    public class JwtTokenGenerator
    {
        private readonly string _jwtKey;
        private readonly string _issuer;

        public JwtTokenGenerator(string jwtKey, string issuer)
        {
            _jwtKey = jwtKey;
            _issuer = issuer;
        }
        // Generate JWT Token khi user dang nhap hoac dang ky 1 account moi
        public string GenerateJwtToken(Account account)
        {
            if (string.IsNullOrEmpty(_jwtKey))
            {
                throw new ArgumentNullException(nameof(_jwtKey), "JWT Key is not configured properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Status", account.Status),
                new Claim("Username", account.Username),
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
