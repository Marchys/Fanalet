using UnityEngine;
using System.Collections;

public class Vasey : ActionE
{
    private GameObject _lighthouse;
    public string firstDialog;
    public string secondDialog;

    // Use this for initialization
	new void Start () {
	    base.Start();
	    _lighthouse = transform.parent.parent.gameObject;
	}

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        waitingForResponse = true;
        Messenger.Publish(new StopMessage());
        Messenger.Publish(new DialogueStartMessage(firstTimeActivation ? firstDialog : secondDialog));
    }
}
