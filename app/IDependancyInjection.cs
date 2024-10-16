using grpc;
using grpc.Services;
using Microsoft.AspNetCore.Components;


namespace app;

public interface IDependancyInjection<in T>:GreeterService
{
    T Dependancy { set; }

    
}
