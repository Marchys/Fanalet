public class StartGuiDestilationMessage
{
    public string ActivationType { get; set; }
    public BaseCaracterStats StatsProtagonist { get; set; }
    public int MessageId { get; set; }
    /// <summary>
    /// Start destilation process
    /// </summary>
    /// <param name="ActivationType">Type of lighthouse activation.</param>
    /// <param name="StatsProtagonist">Protagonist stats.</param>
    public StartGuiDestilationMessage(string activationType, BaseCaracterStats statsProtagonist, int messageId)
    {
        ActivationType = activationType;
        StatsProtagonist = statsProtagonist;
        MessageId = messageId;
    }

}

