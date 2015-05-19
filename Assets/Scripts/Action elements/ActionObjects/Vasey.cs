using UnityEngine;
using System.Collections;

public class Vasey : ActionE, IHandle<DialogueEndMessage>, IHandle<EndPayLighthouseMessage>
{
    private GameObject _lighthouse;
    public TextAsset TextFileDialogue1;
    public TextAsset TextFileDialogue2;
    private BaseProtagonistStats Stats;
    private string[] _firstDialog;
    private string[] _secondDialog;
    private bool _activated = false;
    private int _idMessage = 0;
    private int _lighthousesActivated = 0;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        if (TextFileDialogue1 != null && TextFileDialogue2 != null)
        {
            _firstDialog = Utils.Lines(TextFileDialogue1.text);
            _secondDialog = Utils.Lines(TextFileDialogue2.text);
        }
        else
        {
            _firstDialog[0] = "---";
            _secondDialog[0] = "---";
        }
        _lighthouse = transform.parent.parent.gameObject;
    }

    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        Stats = stats;
        base.ExecuteAction(stats);
        if (minotaurChasing) return;
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        Messenger.Publish(new DialogueStartMessage(!_activated ? _firstDialog : _secondDialog, _idMessage));
    }


    public void Handle(DialogueEndMessage message)
    {
        if (message.MessageId == _idMessage && !_activated)
        {
            Messenger.Publish(new StartPayLighthouseMessage(Stats, Constants.Prices.LighthousesActivation[_lighthousesActivated], _idMessage));
        }
        else if(message.MessageId == _idMessage && _activated)  Messenger.Publish(new ContinueMessage());
    }

    public void Handle(EndPayLighthouseMessage message)
    {

        if (message.ActivationType.RedHearts != 0 || message.ActivationType.BlueHearts != 0 ||
               message.ActivationType.YellowHearts != 0)
        {

            if (message.MessageId == _idMessage)
            {
                _activated = true;
                _lighthouse.GetComponent<LighthouseStructure>().ActivateLighthouse(message.ActivationType, _lighthousesActivated);
                Messenger.Publish(new ContinueMessage());
                _idMessage = 0;
            }
            else
            {
                _lighthousesActivated++;
            }

        }
        else if (message.MessageId == _idMessage)
        {
            Messenger.Publish(new ContinueMessage());
            _idMessage = 0;
        }

    }
}
