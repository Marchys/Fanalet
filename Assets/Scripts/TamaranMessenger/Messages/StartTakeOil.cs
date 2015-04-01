public class StartTakeOil
{

    public BaseCaracterStats StatsProtagonist { get; set; }
    public string ActivationType { get; set; }
    public int OilDestilated { get; set; }
    public int MessageId { get; set; }

    public StartTakeOil(BaseCaracterStats statsProtagonist, string activationType, int oilDestilated,int messageID)
    {
        MessageId = messageID;
        OilDestilated = oilDestilated;
        ActivationType = activationType;
        StatsProtagonist = statsProtagonist;
    }

}
