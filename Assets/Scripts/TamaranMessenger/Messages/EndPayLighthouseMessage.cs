
public class EndPayLighthouseMessage
{
    public BaseCaracterStats ActivationType { get; set; }
    public int MessageId { get; set; }
    public EndPayLighthouseMessage(BaseCaracterStats activationType,int messageId)
    {
        ActivationType = activationType;
        MessageId = messageId;
    }
}

