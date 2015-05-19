public class StartPayLighthouseMessage
{
    public int OilToPay{ get; set; }
    public BaseProtagonistStats StatsProtagonist { get; set; }
    public int MessageId { get; set; }
    public StartPayLighthouseMessage(BaseProtagonistStats statsProtagonist, int oilToPay, int messageID)
    {
        StatsProtagonist = statsProtagonist;
        OilToPay = oilToPay;
        MessageId = messageID;
    }
}
