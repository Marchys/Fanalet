using UnityEngine;

public class OldMinerBrother : ActionE, IHandle<DialogueEndMessage>
{

    private int _idMessage;
    public TextAsset TextFileDialogue;
    private string[] _firstDialog;
    public GameObject OldManGameObject;

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
        OldManGameObject.GetComponent<Animator>().SetInteger("OldManState", 2);
        Messenger.Publish(new DialogueStartMessage(_firstDialog, _idMessage));
    }

    public void Handle(DialogueEndMessage message)
    {
        if (message.MessageId != _idMessage) return;
        OldManGameObject.GetComponent<Animator>().SetInteger("OldManState", 0);
        Messenger.Publish(new ContinueMessage());
    }
}
