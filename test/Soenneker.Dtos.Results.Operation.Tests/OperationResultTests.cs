using System.Net;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Soenneker.Tests.Unit;
using System.Threading;

namespace Soenneker.Dtos.Results.Operation.Tests;

public sealed class OperationResultTests : UnitTest
{
    [Test]
    public async ValueTask Generic_success_serializes_one_typed_value_with_system_text_json(CancellationToken cancellationToken)
    {
        OperationResult<string> result = OperationResult.Success("ready", HttpStatusCode.Created);

        string systemTextJson = System.Text.Json.JsonSerializer.Serialize(result);

        using JsonDocument document = JsonDocument.Parse(systemTextJson);
        await Assert.That(document.RootElement.GetProperty("value").GetString()).IsEqualTo("ready");
        await Assert.That(document.RootElement.EnumerateObject().Count(property => property.Name == "value")).IsEqualTo(1);

    }
}
