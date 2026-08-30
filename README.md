[![](https://img.shields.io/nuget/v/soenneker.dtos.results.operation.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.results.operation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.results.operation/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.results.operation/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dtos.results.operation.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dtos.results.operation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dtos.results.operation/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dtos.results.operation/actions/workflows/codeql.yml)

# Soenneker.Dtos.Results.Operation

A result envelope for service and API operations. It carries an HTTP-style status code plus either a typed success value or problem details, with support for `System.Text.Json` and Newtonsoft.Json.

## Install

```bash
dotnet add package Soenneker.Dtos.Results.Operation
```

## Create results

```csharp
using System.Net;
using Soenneker.Dtos.Results.Operation;

OperationResult<OrderDto> created = OperationResult.Success(
    order,
    HttpStatusCode.Created);

OperationResult<OrderDto> missing = OperationResult.Fail<OrderDto>(
    "Order not found",
    "No order exists with id 42.",
    HttpStatusCode.NotFound);

OperationResult noContent = OperationResult.Empty();
```

Use the generic overloads when the caller needs a strongly typed `Value`. Use the non-generic overloads for commands with no response body.

## Consume a result

```csharp
if (result.Succeeded)
{
    OrderDto? order = result.Value;
}
else
{
    Console.WriteLine(result.Problem?.Detail);
}
```

`Succeeded` means `Problem` is null; `Failed` is its inverse. Neither property is serialized, and neither examines `StatusCode` or `Value`. A default instance therefore counts as successful even though its status code is `0` and its value is null.

The factory methods keep `StatusCode` and `Problem.Status` aligned for failures. The public properties remain mutable, so callers can create contradictory states if they bypass the factories. `StatusCode` is payload metadata only and does not set an actual HTTP response status.

`Empty<T>()` defaults to 204 and carries a default value. Null serialization follows the options configured for the selected serializer.
