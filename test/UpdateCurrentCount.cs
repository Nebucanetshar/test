//using app.Wrapper.Fluxor;
//using Fluxor;
//using Moq;

//[Fact]
//public async Task TestEffectMethod()
//{
//    // Arrange
//    var mockDispatcher = new Mock<IDispatcher>();
//    var effect = new Effet();
//    var action = new ActionInput();

//    // Act
//    await effect.CallBroadcast(action, mockDispatcher.Object);

//    // Assert
//    mockDispatcher.Verify(d => d.Dispatch(It.IsAny<>()), Times.Once); // what put in IsAny ? 
//}