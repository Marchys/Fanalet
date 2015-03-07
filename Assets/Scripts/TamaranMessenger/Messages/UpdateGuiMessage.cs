public class UpdateGuiMessage
{
    public BaseCaracterStats UpdatedProtaStats { get; set;}

    public UpdateGuiMessage(BaseCaracterStats updatedProtaStats)
    {
        UpdatedProtaStats = updatedProtaStats;
    }
}
