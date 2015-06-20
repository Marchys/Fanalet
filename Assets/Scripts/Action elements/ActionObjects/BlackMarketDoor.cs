using UnityEngine;
using System.Collections;

public class BlackMarketDoor : ActionE, IHandle<EndTransitionGuiMessage>
{
    public Vector2 marketLocation;

    private int _idMessage;
    private bool _firstTransition = true;

    // Update is called once per frame
    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        _firstTransition = true;
        Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.In, _idMessage));
    }

    public override void Handle(MinotaurChaseMessage message)
    {

    }

    public void Handle(EndTransitionGuiMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (_firstTransition)
        {
            Camera.main.transform.position = marketLocation;
            Prota.transform.position = marketLocation;
            Messenger.Publish(new ProtaEntersStructureMessage());
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
