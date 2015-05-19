public class StartBlackShopMessage  
{
	public int MessageId { get; set; }
    public BaseProtagonistStats ProtaStats { get; set; }

    public StartBlackShopMessage(int messageID, BaseProtagonistStats protaStats)
    {
        MessageId = messageID;
        ProtaStats = protaStats;
    }
}

