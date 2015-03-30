public class StartPayLighthouseMessage
{
    public int OilToPay{ get; set; }
    public BaseCaracterStats StatsProtagonist { get; set; }
    public int MessageId { get; set; }
    public StartPayLighthouseMessage(BaseCaracterStats statsProtagonist, int oilToPay, int messageID)
    {
        StatsProtagonist = statsProtagonist;
        OilToPay = oilToPay;
        MessageId = messageID;
    }
}
