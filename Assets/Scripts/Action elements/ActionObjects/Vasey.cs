using UnityEngine;
using System.Collections;

public class Vasey : ActionE
{
    private GameObject _lighthouse;
    public TextAsset TextFileDialogue1;
    public TextAsset TextFileDialogue2;
    private string[] _firstDialog;
    private string[] _secondDialog;

    // Use this for initialization
	new void Start () {
	    base.Start();
	    if (TextFileDialogue1 != null && TextFileDialogue2 != null)
	    {
	        _firstDialog = (TextFileDialogue1.text.Split('\n'));
	        _secondDialog = (TextFileDialogue2.text.Split('\n'));
	    }
	    else
	    {
	        _firstDialog[0] = "---";
	        _secondDialog[0] = "---";
	    }
	    _lighthouse = transform.parent.parent.gameObject;
	}

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        waitingForResponse = true;
        Messenger.Publish(new StopMessage());
        Messenger.Publish(new DialogueStartMessage(firstTimeActivation ? _firstDialog : _secondDialog));
    }
}
