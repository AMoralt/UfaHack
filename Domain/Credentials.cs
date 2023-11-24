using System.Text.Json.Serialization;
using O2GEN.Authorization;

namespace Domain.AggregationModels;

public class Credentials
{
    [JsonIgnore]
    public int Id { get; set; }
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }
    public string Token { get; set; }
    public string RoleCode { get; set; }
    public TokenException TokenException { get; set; }
    public string DisplayName { get; set; }
}