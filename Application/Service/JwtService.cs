using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.AggregationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using O2GEN.Authorization;

namespace Application.Service;

public class JwtService : IJwtService
{
    private readonly JwtConfig _jwtConfig;

    public JwtService(IOptions<JwtConfig> options)
    {
        _jwtConfig = options.Value;
    }

    public string GenerateJwtToken(Credentials user)
    {
        // Set our tokens claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim("id", user.Id.ToString()),
            new Claim("uname", user.DisplayName.ToString()),
            new Claim("role", user.RoleCode.ToString())
        };

        // Create the credentials used to generate the token
        var credentials = new SigningCredentials(
            // Get the secret key
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
            // Use HS256 algorithm
            SecurityAlgorithms.HmacSha256);

        // Generate the Jwt Token
        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            signingCredentials: credentials,
            notBefore: DateTime.Now,
            // Expire if not used
            expires: DateTime.Now.AddDays(1)//AddHours(24)
        );

        // Return the generated token
        return new JwtSecurityTokenHandler().WriteToken(token);
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
            output.TokenException = TokenException.Ok;
            return output;
        }
        catch (System.Exception e)
        {
            output.TokenException = TokenException.WrongToken;
            return output;
        }
    }
}