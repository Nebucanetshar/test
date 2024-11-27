namespace app;

public class ResponseWrapperViewModel<T_VIEWMODEL> : ResponseWrapperBase
{
    public T_VIEWMODEL? Content { get; set; }

    private ResponseWrapperViewModel (ResponseBase response)
    {
        Content = default;
    }
    
    public static ResponseWrapperViewModel<T_VIEWMODEL>Create<T_DTO>(ResponseBase<T_DTO> response, Func<T_DTO,T_VIEWMODEL> transformer)
    {
        ResponseWrapperViewModel<T_VIEWMODEL> responseWrapper = new(response);
        responseWrapper.Content = transformer(response.Result);

        return responseWrapper;
    }
}
