public class StartDestilationMessage
{
    public string ActivationType { get; set; }
    public BaseCaracterStats StatsProtagonist { get; set; }

    /// <summary>
    /// Start destilation process
    /// </summary>
    /// <param name="ActivationType">Type of lighthouse activation.</param>
    /// <param name="StatsProtagonist">Protagonist stats.</param>
    public StartDestilationMessage(string activationType, BaseCaracterStats statsProtagonist)
    {
        ActivationType = activationType;
        StatsProtagonist = statsProtagonist;
    }

}

