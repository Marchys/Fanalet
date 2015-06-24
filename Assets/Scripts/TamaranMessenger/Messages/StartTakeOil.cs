public class StartTakeOil
{

    public BaseProtagonistStats StatsProtagonist { get; set; }
    public string ActivationType { get; set; }
    public int OilDestilated { get; set; }
    public bool DestilationFinished { get; set; }
    public int MessageId { get; set; }

    public StartTakeOil(BaseProtagonistStats statsProtagonist, string activationType, int oilDestilated, int messageID, bool destilationFinished)
    {
        MessageId = messageID;
        OilDestilated = oilDestilated;
        ActivationType = activationType;
        StatsProtagonist = statsProtagonist;
        DestilationFinished = destilationFinished;
    }

}
