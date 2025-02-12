using Bunit;
using Fluxor;
using Moq;
using app.Wrapper.Fluxor;
using Microsoft.Extensions.DependencyInjection;
using app.Components.Pages;
using Microsoft.Extensions.Options;
using Syncfusion.Blazor;
using Microsoft.JSInterop;

namespace test;

public class ComponentTests : TestContext
{
    [Fact]
    public void Aceg_DispatchIncrement()
    {
        // initialisation pour SyncfusionBlazorServices
        var options = Options.Create(new GlobalOptions());
        var jsRuntime = new Mock<IJSRuntime>();
        var syncfusion = new SyncfusionBlazorService(options, jsRuntime.Object);

        //arange : initialisation des variables
        var state = new Mock<IState<State>>();
        state.Setup(o => o.Value).Returns(new State());

        var dispatch = new Mock<IDispatcher>();
        var store = new Mock<IStore>();

        Services.AddSingleton(state.Object);
        Services.AddSingleton(dispatch.Object);
        Services.AddSingleton(store.Object);
        Services.AddSingleton(syncfusion);

        var components = RenderComponent<Aceg>();

        // act : action des variables
        components.Find("button").Click();

        //assert : affirmation du résultat attendu 
        dispatch.Verify(o => o.Dispatch(It.IsAny<CallAction>()), Times.Once());
    }
}
