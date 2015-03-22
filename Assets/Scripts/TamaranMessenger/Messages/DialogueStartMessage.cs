using System.Collections.Generic;

public class DialogueStartMessage{

    public string[] DialogText { get; set; }
    public int MessageId { get; set; }

    public DialogueStartMessage(string[] dialogText, int  messageID)
    {
        DialogText = dialogText;
        MessageId = messageID;
    }
   
}
