using System;

public class RedHeartStats : BaseCaracterStats {

	public RedHeartStats()
    {
        EntityName = "Red Heart";
        EntityDescription = "A very basic enemy heart";
	    RedHearts = 1;
    }

    public override void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
         throw new NotImplementedException();
    }
   
}
