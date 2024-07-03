using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace wasm;

public class CustomBoundary : ErrorBoundary
{
    [Inject]
    private IWebAssemblyHostEnvironment Environment { get; set; }

    protected override Task OnErrorAsync(Exception exception)
    {
        if (Environment.IsProduction())
            return base.OnErrorAsync(exception);
        else
            return Task.CompletedTask;

    }
}
