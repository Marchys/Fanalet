using UnityEngine;
using System.Collections;

public class DarkTrader : ActionE, IHandle<DialogueEndMessage>, IHandle<EndBlackShopMessage>
{
    public TextAsset TextFileDialogue1;
    public TextAsset TextFileDialogue2;
    private string[] _firstDialog;
    private string[] _secondDialog;
    private int _idMessage = 0;
    private bool _bought = false;
    private BaseProtagonistStats protaStats;

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
    }
    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        protaStats = stats;
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        Messenger.Publish(new DialogueStartMessage(!_bought ? _firstDialog : _secondDialog, _idMessage));
    }

    public override void Handle(MinotaurChaseMessage message)
    {

    }


    public void Handle(DialogueEndMessage message)
    {
        if (message.MessageId == _idMessage && !_bought)
        {
            Messenger.Publish(new StartBlackShopMessage(_idMessage,protaStats));
        }
        else if (message.MessageId == _idMessage && _bought) Messenger.Publish(new ContinueMessage());
    }

    public void Handle(EndBlackShopMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (message.Sucess)
        {
            _bought = true;
        }
        Messenger.Publish(new ContinueMessage());
    }
}
