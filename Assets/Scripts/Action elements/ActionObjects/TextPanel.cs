using UnityEngine;
using System.Collections;

public class TextPanel : ActionE, IHandle<DialogueEndMessage>
{
    private int _idMessage;
    public TextAsset TextFileDialogue;
    private string[] _firstDialog;

    protected override void Start()
    {
        base.Start();
        if (TextFileDialogue != null)
        {
            _firstDialog = Utils.Lines(TextFileDialogue.text);
        }
        else
        {
            _firstDialog[0] = "---";
        }
    }

    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        Messenger.Publish(new DialogueStartMessage(_firstDialog, _idMessage));
    }

    public void Handle(DialogueEndMessage message)
    {
        if (message.MessageId != _idMessage) return;
        Messenger.Publish(new ContinueMessage());
    }
}
