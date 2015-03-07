using UnityEngine;
using System.Collections;

public class Vasey : ActionE
{

    private GameObject _lighthouse;

    // Use this for initialization
	new void Start () {
	    base.Start();
	    _lighthouse = transform.parent.parent.gameObject;
	}

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        Messenger.Publish(new StopMessage()); 
    }
}
