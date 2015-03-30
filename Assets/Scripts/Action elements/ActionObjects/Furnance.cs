using UnityEngine;
using System.Collections;

public class Furnance : ActionE
{

    private string _activationType;

    public void SetLighthousetype(BaseCaracterStats ActivationStats)
    {
         if (ActivationStats.RedHearts != 0)
         {
             _activationType = "red";
         }
        else if (ActivationStats.BlueHearts != 0)
        {
            _activationType = "blue";
        }
        else if (ActivationStats.YellowHearts != 0)
        {
            _activationType = "yellow";
        }
    }
    
    public override void ExecuteAction(BaseCaracterStats stats)
    {
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        Messenger.Publish(new StartDestilationMessage(_activationType,stats));
    }
}
