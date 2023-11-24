using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exception;
using Application.Service;
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
    private readonly IJwtService _jwtService;
    
    public LoginQueryHandler(ICredentialRepository credentialRepository, IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _credentialRepository = credentialRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<Credentials> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _credentialRepository.GetAsync(request.Login, request.Password, cancellationToken);

        if (result is null)
            throw new UnauthorizedException("User not found");

        var user = new Credentials
        {
            Login = request.Login,
            Id = result.Id,
            DisplayName = $"{result.Surname} {(!string.IsNullOrEmpty(result.GivenName) ? $"{result.GivenName.Substring(0, 1)}." : "")}{(!string.IsNullOrEmpty(result.MiddleName) ? $"{result .MiddleName.Substring(0, 1)}." : "")}",
            RoleCode = result.RoleCode
        };
        
        user.Token = _jwtService.GenerateJwtToken(user);
        
        return user;
    }
}

public record LoginQuery(
    string Login,
    string Password) :  IRequest<Credentials>;