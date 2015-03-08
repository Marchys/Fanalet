using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviourEx, IHandle<DialogueStartMessage>
{

    private Text dialogueBox;
    private string text;
    
    // Use this for initialization
	void Start () {
        dialogueBox = transform.Find("dialogText").GetComponent<Text>();
	    dialogueBox.text ="hello";
	}

    public void Handle(DialogueStartMessage message)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        text = message.DialogText;
    }

    IEnumerator TypeText()
    {
        foreach (char letter in text.ToCharArray())
        {
            if (letter != '|')
            {
                dialogueBox.text += letter;
            }else  yield break;

            yield return new WaitForSeconds(0.000001f);
        }
    }
}
