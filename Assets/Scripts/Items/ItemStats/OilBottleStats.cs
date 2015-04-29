using System;

public class OilBottleStats : BaseCaracterStats
{

    public OilBottleStats()
    {
        EntityName = "OilBottle";
        EntityDescription = "A uncommon enemy heart";
        OiLife = 5;
    }

    public override void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
         throw new NotImplementedException();
    }
   
}
