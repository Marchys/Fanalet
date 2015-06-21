public class LoadingTick
{
    public int CurrentLoaded { get; set; }
    public int Total { get; set; }

    public LoadingTick(int currentLoaded, int total)
    {
        CurrentLoaded = currentLoaded;
        Total = total;
    }
}
