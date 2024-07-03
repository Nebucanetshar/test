namespace wasm.fluxor;

public class Action
{
    public ResultResponseViewModel? ResponseServer;
    public CounterViewModel? Content { get; }

    public ActionOutput(ResultResponseViewModel responseServer)
    {
        ResponseServer = responseServer;
    }
    public ActionOutput(CounterViewModel content)
    {
        Content = content;
    }

}

public class ActionInput
{
    public string Counter { get; set; }

    public ActionInput(string counter)
    {
        Counter = counter;
    }
}
