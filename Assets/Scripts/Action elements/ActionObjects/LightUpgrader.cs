using UnityEngine;
using System.Collections;

public class LightUpgrader : ActionE, IHandle<EndUpgradeGuiMessage>, IHandle<DialogueEndMessage>
{
    private int _idMessage = 0;
    private readonly BaseCaracterStats[] _upgrades = { Constants.Prices.UpgradeOne, Constants.Prices.UpgradeTwo, Constants.Prices.UpgradeThree, Constants.Prices.UpgradeFour };
    public int LighthousesActivated;
    private BaseProtagonistStats _stats;

    private bool _upgradeBought = false;

    public TextAsset TextFileDialogue1;
    public TextAsset TextFileDialogue2;
    private string[] _firstDialog;
    private string[] _secondDialog;

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
        if (Blocked) return;
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        _stats = stats;
        Messenger.Publish(new DialogueStartMessage(_upgradeBought ? _secondDialog : _firstDialog, _idMessage));
    }


    public void Handle(DialogueEndMessage message)
    {
        if (_idMessage != message.MessageId) return;
        if (_upgradeBought)
        {
            Messenger.Publish(new ContinueMessage());
            return;
        }
        Messenger.Publish(new StartUpgradeGuiMessage(_idMessage, _upgrades[LighthousesActivated], _stats));
    }


    public void Handle(EndUpgradeGuiMessage message)
    {
        if (_idMessage != message.MessageId) return;
        if (message.Upgraded)
        {
            _upgradeBought = true;
            EAnimator.SetInteger("animationState", 0);
        }
        Messenger.Publish(new ContinueMessage());

    }

}
