using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviourEx, IHandle<DialogueStartMessage>
{

    private Text dialogueBox;
    public Image showAvailabeAction;
    private string[] currentDialogLines;
    private int currentLine;
    private int _messageId;
    private bool _typing = false;
    
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
        showAvailabeAction.gameObject.SetActive(false);
        currentDialogLines = message.DialogText;
        _messageId = message.MessageId;
        currentLine = 0;
        StartCoroutine(DetectInput());
        NextLine();
    }

    public void NextLine()
    {
        if (_typing) return;
        if (currentDialogLines.Length > currentLine)
        {
            dialogueBox.text = "";
            StopCoroutine("TypeText");
            StartCoroutine(TypeText(currentDialogLines, currentLine));
        }
        else
        {
            StopAllCoroutines();
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
            _typing = true;
            showAvailabeAction.gameObject.SetActive(false);
            foreach (char letter in text[line].ToCharArray())
            {
                dialogueBox.text += letter;
                yield return new WaitForSeconds(0.01f);
            }
            showAvailabeAction.gameObject.SetActive(true);
            _typing = false;
    }

    IEnumerator DetectInput()
    {
        while (true)
        {
            if (Input.GetButtonDown("accio"))
            {
                NextLine();
            }
            yield return null;
        }
       
    }
}
