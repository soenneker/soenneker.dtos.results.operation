[![](https://img.shields.io/nuget/v/soenneker.dtos.results.operation.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.results.operation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.results.operation/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.results.operation/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dtos.results.operation.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.results.operation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.results.operation/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.results.operation/actions/workflows/codeql.yml)

# Soenneker.Dtos.Results.Operation

Represents the standardized outcome of an operation, containing either a successful result value or detailed error information in the form of a `ProblemDetailsDto`.

## Install

```bash
dotnet add package Soenneker.Dtos.Results.Operation
```

## What you get

- `OperationResult` — Represents the standardized outcome of an operation, containing either a successful result value or detailed error information in the form of a `ProblemDetailsDto`.
- `OperationResult<T>` — Represents the standardized outcome of an operation with a strongly typed success value or machine-readable problem details.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `OperationResult.Succeeded` | Indicates whether the operation completed without problem details. This convenience property is not serialized. | Indicates whether the operation completed without problem details. This convenience property is not serialized. |
| `OperationResult.StatusCode` | HTTP status code associated with the operation result. This value reflects the outcome of the operation, such as 200 for success or 400 for a client error. | HTTP status code associated with the operation result. This value reflects the outcome of the operation, such as 200 for success or 400 for a client error. |
| `OperationResult.Value` | Value returned when the operation succeeds. This property is `null` when the operation fails. | Value returned when the operation succeeds. This property is `null` when the operation fails. |
| `OperationResult.Problem` | Problem details describing the error when the operation fails. This property is `null` when the operation succeeds. | Problem details describing the error when the operation fails. This property is `null` when the operation succeeds. |
| `OperationResult.Failed` | Indicates whether the operation contains problem details. This convenience property is not serialized. | Indicates whether the operation contains problem details. This convenience property is not serialized. |
| `OperationResult.Success(value, statusCode)` | Creates a successful result containing the supplied payload. | Returns `OperationResult<T>`. |
| `OperationResult.Success(statusCode)` | Creates a successful result containing the supplied payload. | The same builder instance, so additional classes or variants can be chained. |
| `OperationResult.Fail(title, detail, statusCode)` | Returns the value produced by fail. | Returns `OperationResult<T>`. |
| `OperationResult.Empty(statusCode)` | Returns the value produced by empty. | Returns `OperationResult<T>`. |
| `OperationResult<T>.Value` | Strongly typed value returned when the operation succeeds. This property is `null` when the operation fails. | Strongly typed value returned when the operation succeeds. This property is `null` when the operation fails. |
