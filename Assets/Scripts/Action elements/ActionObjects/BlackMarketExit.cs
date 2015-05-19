using UnityEngine;
using System.Collections;

public class BlackMarketExit : ActionE
{
    public Vector2 exitLocation;
    // Update is called once per frame
    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        base.ExecuteAction(stats);
        Camera.main.transform.position = exitLocation;
        Prota.transform.position = exitLocation;
        Messenger.Publish(new ProtaExitsStructureMessage());
    }

    public override void Handle(MinotaurChaseMessage message)
    {

    }

}
