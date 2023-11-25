using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Exception;
using Application.Service;
using Domain;
using Domain.AggregationModels;
using Infrastructure.Contracts;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using O2GEN.Authorization;
using O2GEN.Models;

public class CreateUserProgressCommandHandler : IRequestHandler<CreateUserProgressCommand, int>
{
    private readonly IUserProgressRepository _userProgressRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateUserProgressCommandHandler(IUserProgressRepository userProgressRepository, IUnitOfWork unitOfWork)
    {
        _userProgressRepository = userProgressRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateUserProgressCommand request, CancellationToken cancellationToken)
    {
        var userProgress = new UserProgress
        {
            UserID = request.UserID,
            CourseID = request.CourseID,
            ModuleID = request.ModuleID,
            CompletionStatus = request.CompletionStatus,
            Score = request.Score
        };
        await _unitOfWork.StartTransaction(cancellationToken);
        var newId = await _userProgressRepository.CreateAsync(userProgress, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return newId;
    }
}

public record CreateUserProgressCommand(
    int UserID, int CourseID, int ModuleID, bool CompletionStatus, int? Score
) : IRequest<int>; 