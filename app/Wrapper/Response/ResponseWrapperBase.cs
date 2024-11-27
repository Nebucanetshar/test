//namespace app;

//public class ResponseWrapperBase
//{
//    public Dictionary<MessageTypeViewModel, IEnumerable<ResultMessageViewModel>> Messages { get; set; }

//    public bool IsSuccess => !HasError && !HasCriticalError;

//    public bool HasErrorOrCriticalError => !IsSuccess;

//    public bool HasError => Messages.Any(o => o.Key == MessageTypeViewModel.Error);

//    public bool HasCriticalError => Messages.Any(o => o.Key == MessageTypeViewModel.CriticalError);

//    public IEnumerable<ResultMessageViewModel> Errors => GetMessages(MessageTypeViewModel.Error);

//    public IEnumerable<ResultMessageViewModel> CriticalErrors => GetMessages(MessageTypeViewModel.CriticalError);

//    public IEnumerable<ResultMessageViewModel> GetMessages(MessageTypeViewModel type)
//    {
//        return Messages.ContainsKey(type)
//            ? Messages[type]
//            : new List<ResultMessageViewModel>();
//    }

//    public IEnumerable<ResultMessageViewModel> GetMessagesAllErrors()
//    {
//        var errors = GetMessages(MessageTypeViewModel.Error).ToList();
//        errors.AddRange(GetMessages(MessageTypeViewModel.CriticalError));

//        return errors;
//    }

//    public string ComputeMessages(MessageTypeViewModel messageType)
//    {
//        var messages = GetMessages(messageType).Select(o => o.Label);
//        return string.Join(Environment.NewLine, messages);
//    }

//    public string ComputeErrorMessage()
//    {
//        var errorMessages = GetMessagesAllErrors().Select(o => o.Label);

//        var errorMessageComputed = string.Join(Environment.NewLine, errorMessages);

//        return errorMessageComputed;
//    }
//}
