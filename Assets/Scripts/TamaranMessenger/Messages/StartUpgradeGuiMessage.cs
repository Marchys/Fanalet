public class StartUpgradeGuiMessage
{
    public int MessageId { get; set; }
    public BaseCaracterStats UpgradeStats { get; set; }
    public BaseCaracterStats ProtaStats { get; set; }
    
    public StartUpgradeGuiMessage(int messageId,  BaseCaracterStats upgradeStats, BaseCaracterStats protaStats)
    {
        MessageId = messageId;
        UpgradeStats = upgradeStats;
        ProtaStats = protaStats;
    }
}
