public class EndBlackShopMessage
{
    public int MessageId { get; set; }
    public bool Sucess { get; set; }

    public EndBlackShopMessage(int messageID, bool sucess)
    {
        MessageId = messageID;
        Sucess = sucess;
    }
}

