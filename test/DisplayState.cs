using Moq;
using Bunit;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using app.Wrapper.Fluxor;
using app.Components.Pages;
using Syncfusion.Blazor;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace test.initial;

///<summary>
///Vérifie que l'état initial est bien affiché 
///</summary>
public class ComponentTests : TestContext
{

    [Fact]
    public void Aceg_DisplayState()
    {
        //arrange : Mock du state avec une valeur initial 
        var state = new Mock<IState<State>>();
        state.Setup(o => o.Value).Returns(new State(5));

        var dispatch = new Mock<IDispatcher>();
        var store = new Mock<IStore>();

        //definition des arguments spécifié de syncfusionBlazorService
        var options = Options.Create(new GlobalOptions());
        var jsRuntime = new Mock<IJSRuntime>();
        var syncfusion = new SyncfusionBlazorService(options, jsRuntime.Object);


        Services.AddSingleton(state.Object);
        Services.AddSingleton(dispatch.Object);
        Services.AddSingleton(store.Object);
        Services.AddSingleton(syncfusion);


        //act : Render le composant 
        var component = RenderComponent<Aceg>();

        //assert : vérifie que l'affichage est correct
        component.MarkupMatches("<h1>Counter; 5</> <button>Increment</button>");
    }
}


