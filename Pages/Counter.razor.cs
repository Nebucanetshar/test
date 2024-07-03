namespace wasm.Pages;

public partial class Counter
{
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
        if (currentCount >= 5)
            throw new Exception(" La valeur est trop haute");
    }
}