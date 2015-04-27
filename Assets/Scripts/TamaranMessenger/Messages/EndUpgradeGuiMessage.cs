public class EndUpgradeGuiMessage 
{
    public int MessageId { get; set; }
    public bool Upgraded { get; set; }

    public EndUpgradeGuiMessage(int messageId, bool upgraded)
    {
        MessageId = messageId;
        Upgraded = upgraded;
    }
}
