using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Soenneker.Attributes.PublicOpenApiObject;
using Soenneker.Dtos.ProblemDetails;

namespace Soenneker.Dtos.Results.Operation;

/// <summary>
/// Represents the standardized outcome of an operation with a strongly typed success value or machine-readable problem details.
/// </summary>
/// <typeparam name="T">
/// The type of the successful result value returned by the operation.
/// </typeparam>
[PublicOpenApiObject]
public sealed class OperationResult<T> : OperationResult
{
    /// <summary>
    /// Strongly typed value returned when the operation succeeds.
    /// This property is <see langword="null"/> when the operation fails.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonProperty("value")]
    public new T? Value { get; set; }
}
