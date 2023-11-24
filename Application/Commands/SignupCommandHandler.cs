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

public class SignupCommandHandler : IRequestHandler<SignupCommand, CredentialsDTO>
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    
    public SignupCommandHandler(ICredentialRepository credentialRepository, IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _credentialRepository = credentialRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<CredentialsDTO> Handle(SignupCommand request, CancellationToken cancellationToken)
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
        
        CredentialsDTO credentialsDto = new CredentialsDTO
        {
            Id = resultId,
            Login = newUser.Login,
            Name = newUser.Name
        };
        
        credentialsDto.Token = _jwtService.GenerateJwtToken(credentialsDto);

        return credentialsDto;
    }
}

public record SignupCommand(
    string Login, string Name, 
    string Email, string Password) :  IRequest<CredentialsDTO>;