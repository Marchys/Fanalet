using System;

public class BlueHeartStats : BaseCaracterStats {

    public BlueHeartStats()
    {
        EntityName = "Blue Heart";
        EntityDescription = "A uncommon enemy heart";
	    BlueHearts = 1;
    }

    public override void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
         throw new NotImplementedException();
    }
   
}
