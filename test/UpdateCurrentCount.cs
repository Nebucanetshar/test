namespace test;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Moq;
using grpc;
using app.Components.Pages;
using Xunit;

public class CallBroadcastTests
{
    private readonly Mock<IClient> mockClient;
    private readonly Mock<IAsyncStreamReader<CounterResponse>> mockResponseStream;
    private readonly Grpc instance;

    public CallBroadcastTests()
    {
        mockClient = new Mock<IClient>();
        mockResponseStream = new Mock<IAsyncStreamReader<CounterResponse>>();
        instance = new Grpc(mockClient.Object);
    }

    [Fact]
    public async Task CallBroadcast_ShouldUpdateCurrentCount_WhenMessagesAreReceived()
    {
        // Arrange
        var messages = new List<CounterResponse>
        {
            new CounterResponse { Count = 1 },
            new CounterResponse { Count = 2 },
            new CounterResponse { Count = 3 }
        };

        mockResponseStream
            .SetupSequence(m => m.MoveNext(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(true)
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        mockResponseStream
            .SetupSequence(m => m.Current)
            .Returns(messages[0])
            .Returns(messages[1])
            .Returns(messages[2]);

        var mockResponse = new Mock<AsyncServerStreamingCall<CounterResponse>>(
            null, Task.FromResult(new Metadata()), () => Status.DefaultSuccess, () => new Metadata(), () => { });

        mockResponse.Setup(x => x.ResponseStream).Returns(mockResponseStream.Object);
        mockClient.Setup(c => c.StartCounter(It.IsAny<CounterRequest>())).Returns(mockResponse.Object);

        // Act
        await instance.CallBroadcast();

        // Assert
        Assert.Equal(3, instance.currentCount);  // Vérifie que le dernier count reçu est 3
    }

    [Fact]
    public async Task CallBroadcast_ShouldHandleRpcException_WhenCancelled()
    {
        // Arrange
        var mockResponse = new Mock<AsyncServerStreamingCall<CounterResponse>>(
            null, Task.FromResult(new Metadata()), () => Status.DefaultSuccess, () => new Metadata(), () => { });

        
        mockClient.Setup(c => c.StartCounter(It.IsAny<CounterRequest>())).Returns(mockResponse.Object);

        // Act and Assert
        await instance.CallBroadcast();  // Ne devrait pas lever d'exception
    }
}
