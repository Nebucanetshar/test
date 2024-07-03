using common.ViewModel;
using common.Wrapper.Response;
using grpc;

namespace common.Wrapper;

public class gRpcCounterServiceClient
{
    public async Task<ResponseWrapperViewModel<CounterViewModel>> DoCreate(string arg)
    {
        CreateRequest request = new CreateRequest();
        var responseServer = await Server.DoCreate(request);
        var responseEffet = ResponseWrapperViewModel<CounterViewModel>.Create(responseServer, dto => new CounterViewModel(dto));

        return responseEffet;
    }
}
