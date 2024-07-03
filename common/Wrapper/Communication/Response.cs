using common.Dto;
using common.ViewModel;

namespace common.Wrapper.Response;

#region Interface
public interface IResponseBase
{
    IEnumerable<ResultMessageDto> Messages { get; }

    object? Result { get; }
}
#endregion 



#region ResponseBase
public abstract class ResponseBase : IResponseBase
{
    public abstract IEnumerable<ResultMessageDto> Messages { get; }
    public virtual object? Result => null!;
    protected ResponseBase()
    {

    }
}

public abstract class ResponseBase<T_DTO> : ResponseBase
{
    public new abstract T_DTO Result { get; }

    protected ResponseBase()
    {

    }
}
#endregion



#region ResponseWrapperBase
public abstract class ResponseWrapperBase
{
    public Dictionary<MessageTypeViewModel, IEnumerable<ResultMessageViewModel>> Messages { get; set; }

    public bool IsSuccess => !HasError && !HasCriticalError;

    public bool HasErrorOrCriticalError => !IsSuccess;

    public bool HasError => Messages.Any(o => o.Key == MessageTypeViewModel.Error);

    public bool HasCriticalError => Messages.Any(o => o.Key == MessageTypeViewModel.CriticalError);

    public IEnumerable<ResultMessageViewModel> Errors => GetMessages(MessageTypeViewModel.Error);

    public IEnumerable<ResultMessageViewModel> CriticalErrors => GetMessages(MessageTypeViewModel.CriticalError);

    public IEnumerable<ResultMessageViewModel> GetMessages(MessageTypeViewModel type)
    {
        return Messages.ContainsKey(type)
            ? Messages[type]
            : new List<ResultMessageViewModel>();
    }

    public IEnumerable<ResultMessageViewModel> GetMessagesAllErrors()
    {
        var errors = GetMessages(MessageTypeViewModel.Error).ToList();
        errors.AddRange(GetMessages(MessageTypeViewModel.CriticalError));

        return errors;
    }

    public string ComputeMessages(MessageTypeViewModel messageType)
    {
        var messages = GetMessages(messageType).Select(o => o.Label);
        return string.Join(Environment.NewLine, messages);
    }

    public string ComputeErrorMessage()
    {
        var errorMessages = GetMessagesAllErrors().Select(o => o.Label);

        var errorMessageComputed = string.Join(Environment.NewLine, errorMessages);

        return errorMessageComputed;
    }
}
#endregion 



#region ResponseWrapperViewModel
public class ResponseWrapperViewModel<T_VIEWMODEL> : ResponseWrapperBase
{
    public T_VIEWMODEL Content { get; set; }

    private ResponseWrapperViewModel(ResponseBase response)

    {
        Content = default!;
    }
    public static ResponseWrapperViewModel<T_VIEWMODEL> Create<T_DTO>(ResponseBase<T_DTO> response, Func<T_DTO, T_VIEWMODEL> transformer)
    {
        ResponseWrapperViewModel<T_VIEWMODEL> reponseWrapper = new(response);

        reponseWrapper.Content = transformer(response.Result);

        return reponseWrapper;
    }
}
#endregion
