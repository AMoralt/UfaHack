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

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CoursesDTO>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCourseCommandHandler(ICoursesRepository coursesRepository, IUnitOfWork unitOfWork)
    {
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CoursesDTO> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Courses
        {
            Title = request.Title,
            Description = request.Description,
            Subject = request.Subject,
            PhotoData = request.PhotoData,
            CreatedBy = request.CreatedBy
        };
        await _unitOfWork.StartTransaction(cancellationToken);
        var resultId = await _coursesRepository.CreateAsync(course, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        CoursesDTO coursesDto = new CoursesDTO
        {
            Id = resultId,
            Title = request.Title,
            Description = request.Description,
            Subject = request.Subject,
            PhotoData = request.PhotoData,
            CreatedBy = request.CreatedBy
        };

        return coursesDto;
    }
}

public record CreateCourseCommand(
    string Title, string Description, 
    string Subject, string PhotoData,
    int CreatedBy) :  IRequest<CoursesDTO>;