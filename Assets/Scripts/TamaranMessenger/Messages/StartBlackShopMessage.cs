public class StartBlackShopMessage  
{
	public int MessageId { get; set; }
    public BaseCaracterStats ProtaStats { get; set; }

    public StartBlackShopMessage(int messageID, BaseCaracterStats protaStats)
    {
        MessageId = messageID;
        ProtaStats = protaStats;
    }
}

