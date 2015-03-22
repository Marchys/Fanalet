using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviourEx, IHandle<DialogueStartMessage>
{

    private Text dialogueBox;
    private string[] currentDialogLines;
    private int currentLine;
    private int _messageId;
    
    // Use this for initialization
	void Start () {
        dialogueBox = transform.Find("dialogText").GetComponent<Text>();
	    dialogueBox.text ="";
	}

    public void Handle(DialogueStartMessage message)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        currentDialogLines = message.DialogText;
        _messageId = message.MessageId;
        currentLine = 0;
        NextLine();
    }

    public void NextLine()
    {
        if (currentDialogLines.Length > currentLine)
        {
            dialogueBox.text = "";
            StopCoroutine("TypeText");
            StartCoroutine(TypeText(currentDialogLines, currentLine));
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            Messenger.Publish(new DialogueEndMessage(_messageId));
        }
        currentLine++;
    }

    IEnumerator TypeText(string[]text, int line)
    {
            foreach (char letter in text[line].ToCharArray())
            {
                dialogueBox.text += letter;
                yield return new WaitForSeconds(0.01f);
            }
    }
}
