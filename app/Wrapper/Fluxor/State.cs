using Fluxor;
using grpc;
using Grpc.Core;

namespace app.Wrapper.Fluxor;

[FeatureState]
public record class State
{ 
    public GrpcFlux? Flux { get; set; }

}

//[FeatureState]
//public record class State
//{
//    public List<AsyncServerStreamingCall<CounterResponse>> Response { get; set; }
//    public State()
//    {
//        Response = new List<AsyncServerStreamingCall<CounterResponse>>();
//    }

//}

/// <summary>
/// pour le conteneur d'injection de dependance AddScoped<IFeature,State>
/// </summary>
/// <typeparam name="TState"></typeparam>
//public interface IFeatures<TState>
//{
//    TState State { get; }
//    void Initialize();
//    void UpdateState(TState newState);
//}

//public class State
//{
//    public int Count { get; set; }
//    public bool IsCounting { get; set; }
//}

//public class Feature : IFeatures<State>
//{
//    private State _state;
//    public State State => _state;
//    public Feature()
//    {
//        _state = new State();
//    }

//    public void Initialize()
//    {
//        _state.Count = 0;
//        _state.IsCounting = false;
//    }

//    public void UpdateState(State newState)
//    {
//        _state = newState;
//    }

//}