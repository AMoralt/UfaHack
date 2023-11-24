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
            throw new UserAlreadyExistsException("User already exists with the same login");
        }
        
        var newUser = new UserData
        {
            Login = request.Login,
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
        await _unitOfWork.StartTransaction(cancellationToken);
        var resultId = await _credentialRepository.CreateAsync(newUser, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        Credentials credentials = new Credentials
        {
            Id = resultId,
            Login = newUser.Login,
            Name = newUser.Name
        };
        
        credentials.Token = _jwtService.GenerateJwtToken(credentials);

        return credentials;
    }
}

public record SignupQuery(
    string Login, string Name, 
    string Email, string Password) :  IRequest<Credentials>;