using UnityEngine;
using System.Collections;

public class Vasey : ActionE, IHandle<DialogueEndMessage>, IHandle<EndPayLighthouseMessage>
{
    private GameObject _lighthouse;
    public TextAsset TextFileDialogue1;
    public TextAsset TextFileDialogue2;
    private BaseCaracterStats Stats;
    private string[] _firstDialog;
    private string[] _secondDialog;
    private bool Activated = false;
    private int idMessage = 0;
    private int oilActivationPrice = 20;

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

    public override void ExecuteAction(BaseCaracterStats stats)
    {
        Stats = stats;
        base.ExecuteAction(stats);
        if (minotaurChasing) return;
        Messenger.Publish(new StopMessage());
        idMessage = GetInstanceID();
        Messenger.Publish(new DialogueStartMessage(!Activated ? _firstDialog : _secondDialog, idMessage));
    }


    public void Handle(DialogueEndMessage message)
    {
        if (message.MessageId == idMessage && !Activated)
        {
            Messenger.Publish(new StartPayLighthouseMessage(Stats, oilActivationPrice, idMessage));
        }
        else if(message.MessageId == idMessage && Activated)  Messenger.Publish(new ContinueMessage());
    }

    public void Handle(EndPayLighthouseMessage message)
    {

        if (message.ActivationType.RedHearts != 0 || message.ActivationType.BlueHearts != 0 ||
               message.ActivationType.YellowHearts != 0)
        {

            if (message.MessageId == idMessage)
            {
                Activated = true;
                _lighthouse.GetComponent<LighthouseStructure>().ActivateLighthouse(message.ActivationType);
                Messenger.Publish(new ContinueMessage());
                idMessage = 0;
            }
            else 
            {
                oilActivationPrice *= 2;
            }

        }
        else if (message.MessageId == idMessage)
        {
            Messenger.Publish(new ContinueMessage());
            idMessage = 0;
        }

    }
}
