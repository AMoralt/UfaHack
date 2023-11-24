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

public class SignupQueryHandler : IRequestHandler<SignupQuery, Credentials>
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    
    public SignupQueryHandler(ICredentialRepository credentialRepository, IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _credentialRepository = credentialRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<Credentials> Handle(SignupQuery request, CancellationToken cancellationToken)
    {
        var existingUser = await _credentialRepository.GetAsync(request.Login, cancellationToken);
        if (existingUser != null)
        {
            // Throw exception or handle accordingly
            throw new UserAlreadyExistsException("User already exists with the same login");
        }
        
        var newUser = new UserData
        {
            
        };
        await _credentialRepository.CreateAsync(newUser, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var credentials = new Credentials
        {
            //Login = newUser.Login,
            Id = newUser.Id,
            //DisplayName = newUser.DisplayName,
            RoleCode = newUser.RoleCode
        };
        
        credentials.Token = _jwtService.GenerateJwtToken(credentials);

        return credentials;
    }
}

public record SignupQuery(
    string Login,
    string Password) :  IRequest<Credentials>;