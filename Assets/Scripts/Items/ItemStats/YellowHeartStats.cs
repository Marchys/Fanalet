using System;

public class YellowHeartStats : BaseCaracterStats {

    public YellowHeartStats()
    {
        EntityName = "Yellow Heart";
        EntityDescription = "A rare enemy heart";
	    YellowHearts = 1;
    }

    public override void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
         throw new NotImplementedException();
    }
   
}