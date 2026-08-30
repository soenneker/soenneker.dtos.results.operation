using System.Net;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Soenneker.Tests.Unit;

namespace Soenneker.Dtos.Results.Operation.Tests;

public sealed class OperationResultTests : UnitTest
{
    [Test]
    public async Task Generic_success_serializes_one_typed_value_with_both_serializers()
    {
        OperationResult<string> result = OperationResult.Success("ready", HttpStatusCode.Created);

        string systemTextJson = System.Text.Json.JsonSerializer.Serialize(result);
        string newtonsoftJson = JsonConvert.SerializeObject(result);

        using JsonDocument document = JsonDocument.Parse(systemTextJson);
        await Assert.That(document.RootElement.GetProperty("value").GetString()).IsEqualTo("ready");
        await Assert.That(document.RootElement.EnumerateObject().Count(property => property.Name == "value")).IsEqualTo(1);

        var newtonsoftObject = Newtonsoft.Json.Linq.JObject.Parse(newtonsoftJson);
        await Assert.That(newtonsoftObject["value"]?.ToObject<string>()).IsEqualTo("ready");
        await Assert.That(newtonsoftObject.Properties().Count(property => property.Name == "value")).IsEqualTo(1);
    }
}
