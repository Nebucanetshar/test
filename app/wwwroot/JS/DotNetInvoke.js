function invokeDotNetMethod() {
    Dotnet.invokeMethodAsync("gRpc.razor.cs", "JsInvokabled ").then(data => {
        debugger;
        console.log("Method invoked succefully", data);
    })
        .catch(error => {
            debugger;
            console.error("Error invoking method", error);
        });
}