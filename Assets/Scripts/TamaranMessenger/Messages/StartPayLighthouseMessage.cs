public class StartPayLighthouseMessage
{
    public int OilToPay{ get; set; }
    public BaseCaracterStats Stats { get; set; }
    public int MessageId { get; set; }
    public StartPayLighthouseMessage(BaseCaracterStats stats, int oilToPay, int messageID)
    {
        Stats = stats;
        OilToPay = oilToPay;
        MessageId = messageID;
    }
}
