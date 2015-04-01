public class EndTakeOil
{
    public int MessageId { get; set; }
    public bool IsOilCollected { get; set; }

    public EndTakeOil (int  messageID, bool isOilCollected)
    {
        MessageId = messageID;
        IsOilCollected = isOilCollected;
    }
}
