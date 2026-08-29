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
    /// Indicates whether the operation completed without problem details. This convenience property is not serialized.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool Succeeded => Problem is null;

    /// <summary>
    /// HTTP status code associated with the operation result.
    /// This value reflects the outcome of the operation, such as 200 for success or 400 for a client error.
    /// </summary>
    [JsonPropertyName("statusCode")]
    [JsonProperty("statusCode")]
    public int StatusCode { get; set; }

    /// <summary>
    /// Value returned when the operation succeeds.
    /// This property is <see langword="null"/> when the operation fails.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonProperty("value")]
    public object? Value { get; set; }

    /// <summary>
    /// Problem details describing the error when the operation fails.
    /// This property is <see langword="null"/> when the operation succeeds.
    /// </summary>
    [JsonPropertyName("problem")]
    [JsonProperty("problem")]
    public ProblemDetailsDto? Problem { get; set; }

    /// <summary>
    /// Indicates whether the operation contains problem details. This convenience property is not serialized.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool Failed => !Succeeded;

    /// <summary>
    /// Creates a successful result containing the supplied payload.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the operation result.</typeparam>
    /// <param name="value">Payload carried by the successful result.</param>
    /// <param name="statusCode">HTTP status code associated with the result.</param>
    /// <returns>The resulting operation Result.</returns>
    [Pure]
    public static OperationResult<T> Success<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new OperationResult<T>
        {
            Value = value,
            StatusCode = (int)statusCode
        };
    }

    /// <summary>
    /// Creates a successful result containing the supplied payload.
    /// </summary>
    /// <param name="statusCode">HTTP status code associated with the result.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
    [Pure]
    public static OperationResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new OperationResult
        {
            StatusCode = (int)statusCode
        };
    }

    /// <summary>
    /// Returns the value produced by fail.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the operation result.</typeparam>
    /// <param name="title">Page title, when available.</param>
    /// <param name="detail">Detail for the fail operation.</param>
    /// <param name="statusCode">HTTP status code associated with the result.</param>
    /// <returns>The resulting operation Result.</returns>
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

    /// <summary>
    /// Returns the value produced by fail.
    /// </summary>
    /// <param name="title">Page title, when available.</param>
    /// <param name="detail">Detail for the fail operation.</param>
    /// <param name="statusCode">HTTP status code associated with the result.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
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

    /// <summary>
    /// Returns the value produced by empty.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the operation result.</typeparam>
    /// <param name="statusCode">HTTP status code associated with the result.</param>
    /// <returns>The resulting operation Result.</returns>
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

    /// <summary>
    /// Returns the value produced by empty.
    /// </summary>
    /// <param name="statusCode">HTTP status code associated with the result.</param>
    /// <returns>The same builder instance, so additional classes or variants can be chained.</returns>
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
