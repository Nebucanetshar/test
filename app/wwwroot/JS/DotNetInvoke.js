function invokeDotNetMethod() {
    Dotnet.invokeMethodAsync("app", "CallBroadcastJs")
        .then(data => {
        
        console.log("Method invoked succefully", data);
    })
        .catch(error => {
            
            console.error("Error invoking method", error);
        });
}