using Bunit;
using Fluxor;
using app.Wrapper.Fluxor;
using Moq;
using Microsoft.Extensions.Options;
using Syncfusion.Blazor;
using Microsoft.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using app.Components.Pages;


namespace test;

public class Components : TestContext
{
    [Fact]
    public void Aceg_UpdateDisplay()
    {
        //initalisation des variable pour SyncfusionBlazorServices
        var options = Options.Create(new GlobalOptions());
        var jsRuntime = new Mock<IJSRuntime>();

        //arrange : initialisation des variables
        var state = new Mock<IState<State>>();
        var currentState = new State(0);
        state.Setup(o => o.Value).Returns(() => currentState);

        var dispatch = new Mock<IDispatcher>();

        Services.AddSingleton(new SyncfusionBlazorService(options, jsRuntime.Object));
        Services.AddSingleton(state.Object);
        Services.AddSingleton(dispatch.Object);

        var component = RenderComponent<Aceg>();

        //act : change l'état du composant
        currentState = new State(10);
        component.Render();

        //assert : vérifie que l'affichage soit mis a jour 
        component.MarkupMatches("<h1>Counter : 10</h1> <boutton> Increment</button>");

    }

}
