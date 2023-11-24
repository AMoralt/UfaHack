using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Domain.AggregationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace O2GEN.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtConfig _jwtConfig;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtConfig> options)
        {
            _next = next;
            _jwtConfig = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var user = ValidateJwtToken(token);
            if (user != null)
            {
                context.Items["User"] = user;
            }

            await _next(context);
        }
        
        public Credentials ValidateJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)) 
            };
            Credentials output = new Credentials();
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                int tmp = 0;
                if (!int.TryParse(jwtToken.Claims.First(c => c.Type == "id").Value, out tmp))
                {
                    return null;
                }
                output.Id = tmp;
                output.Login = jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                output.DisplayName = jwtToken.Claims.First(c => c.Type == "uname").Value;
                output.RoleCode  = jwtToken.Claims.First(c => c.Type == "role").Value;
                output.TokenException = Authorization.TokenException.Ok;
                return output;
            }
            catch (Exception e)
            {
                output.TokenException = Authorization.TokenException.WrongToken;
                return output;
            }
        }
    }
}
