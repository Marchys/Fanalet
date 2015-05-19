public class StartUpgradeGuiMessage
{
    public int MessageId { get; set; }
    public BaseCaracterStats UpgradeStats { get; set; }
    public BaseProtagonistStats ProtaStats { get; set; }

    public StartUpgradeGuiMessage(int messageId, BaseCaracterStats upgradeStats, BaseProtagonistStats protaStats)
    {
        MessageId = messageId;
        UpgradeStats = upgradeStats;
        ProtaStats = protaStats;
    }
}
