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

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CoursesDTO>>
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetCoursesQueryHandler(ICoursesRepository coursesRepository, IUnitOfWork unitOfWork)
    {
        _coursesRepository = coursesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CoursesDTO>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var result = await _coursesRepository.GetAllAsync(request.Limit, request.Offset, cancellationToken);
        
        if (!result.Any())
            throw new Exception($"There is no in repository");

        return result.Select(x => new CoursesDTO
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Subject = x.Subject,
            PhotoData = x.PhotoData,
            CreatedBy = x.CreatedBy,
            CreationDate = x.CreationDate,
            UpdatedDate = x.UpdatedDate
        });
    }
}

public record GetCoursesQuery(
    int Limit,
    int Offset) :  IRequest<IEnumerable<CoursesDTO>>;