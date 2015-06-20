using UnityEngine;
using System.Collections;

public class LighthouseInterior : MonoBehaviourEx, IHandle<EndTransitionGuiMessage>
{

    public GameObject LighthouseRoom;
   
    private int _idMessage;
    private bool _firstTransition = true;
    private Vector2 _targetLocation;
    private GameObject _prota;

    private void Start()
    {
        _idMessage = GetInstanceID();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Prota"))
        {
            Messenger.Publish(new StopMessage());
            _targetLocation = new Vector2(LighthouseRoom.transform.position.x + 12, LighthouseRoom.transform.position.y-9.25f);
            _prota = other.gameObject;
            _firstTransition = true;
            Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.In, _idMessage));
        }
    }

    public void Handle(EndTransitionGuiMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (_firstTransition)
        {
            _prota.transform.position = _targetLocation;
            Camera.main.transform.position = new Vector2(_targetLocation.x, _targetLocation.y - 1.25f);
            Messenger.Publish(new ProtaExitsStructureMessage());
            StartCoroutine(ShowAgain());
            _firstTransition = false;
        }
        else
        {
            Messenger.Publish(new ContinueMessage());
        }

    }

    IEnumerator ShowAgain()
    {
        yield return new WaitForSeconds(0.7f);
        Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.Out, _idMessage));
    }
}
