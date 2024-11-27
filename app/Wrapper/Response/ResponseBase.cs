namespace app;

public abstract class ResponseBase 
{
   
}

public abstract class ResponseBase<T_DTO> : ResponseBase
{
    public new abstract T_DTO Result { get; }
    protected ResponseBase() { }

}