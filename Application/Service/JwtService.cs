﻿using System.IdentityModel.Tokens.Jwt;
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

    public string GenerateJwtToken(CredentialsDTO user)
    {
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim("id", user.Id.ToString()),
            new Claim("uname", user.Name.ToString())
        };

        
        var credentials = new SigningCredentials(
           
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
            
            SecurityAlgorithms.HmacSha256);

        
        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            signingCredentials: credentials,
            notBefore: DateTime.Now,
            
            expires: DateTime.Now.AddDays(1)
        );

        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public CredentialsDTO ValidateJwtToken(string token)
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
        CredentialsDTO output = new CredentialsDTO();
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
            output.Name = jwtToken.Claims.First(c => c.Type == "uname").Value;
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