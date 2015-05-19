public class StartGuiDestilationMessage
{
    public string ActivationType { get; set; }
    public BaseProtagonistStats StatsProtagonist { get; set; }
    public int MessageId { get; set; }
    /// <summary>
    /// Start destilation process
    /// </summary>
    /// <param name="ActivationType">Type of lighthouse activation.</param>
    /// <param name="StatsProtagonist">Protagonist stats.</param>
    public StartGuiDestilationMessage(string activationType, BaseProtagonistStats statsProtagonist, int messageId)
    {
        ActivationType = activationType;
        StatsProtagonist = statsProtagonist;
        MessageId = messageId;
    }

}

