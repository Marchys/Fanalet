public class EndGuiDestilationMessage
{
    public int MessageId { get; set; }
    public BaseCaracterStats ModifiedStats { get; set; }

    public EndGuiDestilationMessage(int messageId,BaseCaracterStats modifiedStats)
    {
        ModifiedStats = modifiedStats;
        MessageId = messageId;
    }
}
