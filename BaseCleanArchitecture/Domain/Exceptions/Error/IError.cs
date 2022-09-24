using System.Text.Json.Serialization;

namespace BaseCleanArchitecture.Domain.Exceptions.Error;

public interface IError
{
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}