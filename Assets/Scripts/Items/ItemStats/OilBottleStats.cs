using System;

public class OilBottleStats : BaseCaracterStats
{

    public OilBottleStats()
    {
        EntityName = "OilBottle";
        EntityDescription = "A bit of oil";
        OiLife = 5;
    }

    public override void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
         throw new NotImplementedException();
    }
   
}
