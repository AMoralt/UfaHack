using Domain.AggregationModels;

namespace Application.Service;

public interface IJwtService
{
    string GenerateJwtToken(Credentials user);
    Credentials ValidateJwtToken(string token);
}