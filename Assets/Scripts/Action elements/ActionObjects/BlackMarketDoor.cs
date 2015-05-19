using UnityEngine;
using System.Collections;

public class BlackMarketDoor : ActionE
{
    public Vector2 marketLocation;
    // Update is called once per frame
	public override void ExecuteAction(BaseProtagonistStats stats)
	{
        base.ExecuteAction(stats);
        Camera.main.transform.position = marketLocation;
        Prota.transform.position = marketLocation;
        Messenger.Publish(new ProtaEntersStructureMessage());
	}

    public override void Handle(MinotaurChaseMessage message)
    {
        
    }
	
}
