using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exception;
using Domain.AggregationModels;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using O2GEN.Authorization;
using O2GEN.Models;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Credentials>
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtConfig _jwtConfig;
    
    public LoginQueryHandler(ICredentialRepository credentialRepository, IUnitOfWork unitOfWork, IOptions<JwtConfig> options)
    {
        _credentialRepository = credentialRepository;
        _unitOfWork = unitOfWork;
        
        _jwtConfig = options.Value;
    }

    public async Task<Credentials> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        //await _unitOfWork.StartTransaction(cancellationToken);
        var result = await _credentialRepository.GetAsync(request.Login, request.Password, cancellationToken);

        if (result is null) ;
            //throw new UnauthorizedException("User not found");

        // var user1 = new Credentials
        // {
        //     Login = request.Login,
        //     Id = result.Id,
        //     DisplayName = $"{result.Surname} {(!string.IsNullOrEmpty(result.GivenName) ? $"{result.GivenName.Substring(0, 1)}." : "")}{(!string.IsNullOrEmpty(result.MiddleName) ? $"{result .MiddleName.Substring(0, 1)}." : "")}",
        //     RoleCode = result.RoleCode
        // };

        var user = new Credentials
        {
            Login = "qwe",
            Id = 1,
            DisplayName = "qwe",
            RoleCode = "sa"
        };
        
        user.Token = GenerateJwtToken(user);
        
        //await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user;
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
}

public record LoginQuery(
    string Login,
    string Password) :  IRequest<Credentials>;