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

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, ModulesDTO>
{
    private readonly IModulesRepository _modulesRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateModuleCommandHandler(IModulesRepository modulesRepository, IUnitOfWork unitOfWork)
    {
        _modulesRepository = modulesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ModulesDTO> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.StartTransaction(cancellationToken);
        var module = new Modules()
        {
            CourseID = request.CourseID,
            Title = request.Title,
            Description = request.Description,
            ModuleOrder = request.ModuleOrder
        };
        var resultId = await _modulesRepository.CreateAsync(module, cancellationToken);
        
        ModulesDTO moduleDto = new ModulesDTO
        {
            Id = resultId,
            Title = request.Title,
            Description = request.Description,
            ModuleOrder = request.ModuleOrder,
            CourseID = request.CourseID
        };

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return moduleDto;
    }
}

public record CreateModuleCommand(
    int CourseID, string Title, 
    string Description, int ModuleOrder):  IRequest<ModulesDTO>;