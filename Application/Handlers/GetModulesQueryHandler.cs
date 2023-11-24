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

public class GetModulesQueryHandler : IRequestHandler<GetModulesQuery, IEnumerable<ModulesDTO>>
{
    private readonly IModulesRepository _modulesRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetModulesQueryHandler(IModulesRepository modulesRepository, IUnitOfWork unitOfWork)
    {
        _modulesRepository = modulesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ModulesDTO>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
    {
        var result = await _modulesRepository.GetAllAsync(request.id, cancellationToken);
        
        if (!result.Any())
            throw new Exception($"There is no in repository");

        return result.Select(x => new ModulesDTO
        {
            Id = x.Id,
            CourseID = x.CourseID,
            Title = x.Title,
            Description = x.Description,
            ModuleOrder = x.ModuleOrder
        });
    }
}

public record GetModulesQuery(int id) :  IRequest<IEnumerable<ModulesDTO>>;