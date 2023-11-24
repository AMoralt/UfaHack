using Domain.AggregationModels;

namespace Application.Service;

public interface IJwtService
{
    string GenerateJwtToken(CredentialsDTO user);
    CredentialsDTO ValidateJwtToken(string token);
}