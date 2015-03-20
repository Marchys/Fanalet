using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviourEx, IHandle<DialogueStartMessage>
{

    private Text dialogueBox;
    
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
        StartCoroutine(TypeText(message.DialogText));
    }

    IEnumerator TypeText(string[]text)
    {
        for (int i=0;i<text.Length;i++)
        {
            foreach (char letter in text[i].ToCharArray())
            {
                dialogueBox.text += letter;
                yield return new WaitForSeconds(0.00001f);
            }
        }
       
    }
}
