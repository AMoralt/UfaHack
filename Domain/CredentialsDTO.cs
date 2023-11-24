using System.Text.Json.Serialization;
using O2GEN.Authorization;

namespace Domain.AggregationModels;

public class CredentialsDTO
{
    [JsonIgnore]
    public int Id { get; set; }
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public TokenException TokenException { get; set; }
    public string Name { get; set; }
}

public enum TokenException
{
    Ok = 0,
    Expired = 1,
    WrongToken = 2
}