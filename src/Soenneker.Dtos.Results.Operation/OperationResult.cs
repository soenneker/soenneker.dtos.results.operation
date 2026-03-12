using System.Diagnostics.Contracts;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Soenneker.Attributes.PublicOpenApiObject;
using Soenneker.Dtos.ProblemDetails;

namespace Soenneker.Dtos.Results.Operation;

/// <summary>
/// Represents the standardized outcome of an operation, containing either a successful result value
/// or detailed error information in the form of a <see cref="ProblemDetailsDto"/>.
/// </summary>
[PublicOpenApiObject]
public class OperationResult
{
    /// <summary>
    /// Gets a value indicating whether the operation completed successfully.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool Succeeded => Problem is null;

    /// <summary>
    /// Gets the HTTP status code associated with the operation result.
    /// This value reflects the outcome of the operation, such as 200 for success or 400 for a client error.
    /// </summary>
    [JsonPropertyName("statusCode")]
    [JsonProperty("statusCode")]
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets the value returned when the operation succeeds.
    /// This property is <see langword="null"/> when the operation fails.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonProperty("value")]
    public object? Value { get; set; }

    /// <summary>
    /// Gets the problem details describing the error when the operation fails.
    /// This property is <see langword="null"/> when the operation succeeds.
    /// </summary>
    [JsonPropertyName("problem")]
    [JsonProperty("problem")]
    public ProblemDetailsDto? Problem { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool Failed => !Succeeded;

    [Pure]
    public static OperationResult<T> Success<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new OperationResult<T>
        {
            Value = value,
            StatusCode = (int)statusCode
        };
    }

    [Pure]
    public static OperationResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new OperationResult
        {
            StatusCode = (int)statusCode
        };
    }

    [Pure]
    public static OperationResult<T> Fail<T>(string title, string detail, HttpStatusCode statusCode)
    {
        return new OperationResult<T>
        {
            StatusCode = (int)statusCode,
            Problem = new ProblemDetailsDto
            {
                Title = title,
                Detail = detail,
                Status = (int)statusCode
            }
        };
    }

    [Pure]
    public static OperationResult Fail(string title, string detail, HttpStatusCode statusCode)
    {
        return new OperationResult
        {
            StatusCode = (int)statusCode,
            Problem = new ProblemDetailsDto
            {
                Title = title,
                Detail = detail,
                Status = (int)statusCode
            }
        };
    }

    [Pure]
    public static OperationResult<T> Empty<T>(HttpStatusCode statusCode = HttpStatusCode.NoContent)
    {
        return new OperationResult<T>
        {
            StatusCode = (int)statusCode,
            Value = default,
            Problem = null
        };
    }

    [Pure]
    public static OperationResult Empty(HttpStatusCode statusCode = HttpStatusCode.NoContent)
    {
        return new OperationResult
        {
            StatusCode = (int)statusCode,
            Value = null,
            Problem = null
        };
    }
}