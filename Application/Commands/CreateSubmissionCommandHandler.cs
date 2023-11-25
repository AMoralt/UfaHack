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

public class CreateUserSubmissionCommandHandler : IRequestHandler<CreateUserSubmissionCommand, int>
{
    private readonly IUserSubmissionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserSubmissionCommandHandler(IUserSubmissionRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateUserSubmissionCommand request, CancellationToken cancellationToken)
    {
        var userSubmission = new UserSubmission
        {
            UserID = request.UserID,
            QuizID = request.QuizID,
            AnswerGiven = request.AnswerGiven,
            IsCorrect = request.IsCorrect
        };

        var createdId = await _repository.CreateAsync(userSubmission, cancellationToken);
        return createdId;
    }
}

public record CreateUserSubmissionCommand(
    int UserID,
    int QuizID,
    string AnswerGiven,
    bool IsCorrect
) : IRequest<int>;