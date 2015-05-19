using UnityEngine;
using System.Collections;

public class LightUpgrader : ActionE, IHandle<EndUpgradeGuiMessage>
{
    private int _idMessage = 0;
    private readonly BaseCaracterStats[] _upgrades = { Constants.Prices.UpgradeOne, Constants.Prices.UpgradeTwo, Constants.Prices.UpgradeThree, Constants.Prices.UpgradeFour };
    public int LighthousesActivated;

    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        if (Blocked) return;
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        Messenger.Publish(new StartUpgradeGuiMessage(_idMessage, _upgrades[LighthousesActivated], stats));
    }


    public void Handle(EndUpgradeGuiMessage message)
    {
        if (_idMessage != message.MessageId) return;
        if (message.Upgraded)
        {
            Blocked = true;
            EAnimator.SetInteger("animationState", 0);
        }
        Messenger.Publish(new ContinueMessage());

    }
}
