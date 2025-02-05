using System.Text.Json.Serialization;
using Grpc.Core;
using grpc;
using System.Text.Json;
namespace app;

public class CounterResponseJsonConverter : JsonConverter<AsyncServerStreamingCall<CounterResponse>>
{
    public override AsyncServerStreamingCall<CounterResponse> Read (ref Utf8JsonReader reader , Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, AsyncServerStreamingCall<CounterResponse> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, new { count = value.ResponseStream }, options);
    }
}
