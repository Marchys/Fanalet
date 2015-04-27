using UnityEngine;
using System.Collections;

public class LightUpgrader : ActionE, IHandle<EndUpgradeGuiMessage>
{
    private int _idMessage = 0;
    private BaseCaracterStats[] _upgrades = new BaseCaracterStats[4];
    public int LighthousesActivated;

    private new void Start()
    {
        base.Start();
        //set upo all 4 possible upgrades
        _upgrades[0] = new BaseCaracterStats();
        _upgrades[0].Attack += 1;
        _upgrades[0].MaxOiLife += 50;
        _upgrades[0].RedHearts -= 10;

        _upgrades[1] = new BaseCaracterStats();
        _upgrades[1].BaseSpeed += 1;
        _upgrades[1].MaxOiLife += 50;
        _upgrades[1].BlueHearts -= 7;

        _upgrades[2] = new BaseCaracterStats();
        _upgrades[2].MaxOiLife += 150;
        _upgrades[2].YellowHearts -= 5;

        _upgrades[3] = new BaseCaracterStats();
        _upgrades[3].Attack += 3;
        _upgrades[3].BaseSpeed += 2;
        _upgrades[3].MaxOiLife += 50;
        _upgrades[3].RedHearts -= 5;
        _upgrades[3].BlueHearts -= 5;
        _upgrades[3].YellowHearts -= 5;

    }

    public override void ExecuteAction(BaseCaracterStats stats)
    {
        if (blocked) return;
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        Messenger.Publish(new StartUpgradeGuiMessage(_idMessage,_upgrades[LighthousesActivated],stats));
    }


    public void Handle(EndUpgradeGuiMessage message)
    {
        if (_idMessage != message.MessageId || !message.Upgraded) return;
        blocked = true;
        _eAnimator.SetInteger("animationState", 0);
    }
}
