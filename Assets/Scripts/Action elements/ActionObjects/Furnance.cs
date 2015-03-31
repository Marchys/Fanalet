using UnityEngine;
using System.Collections;

public class Furnance : ActionE, IHandle<EndGuiDestilationMessage>
{

    private string _activationType;
    private int idMessage = 0;

    void Start()
    {
        new CountDown();
    }
    
    
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
        idMessage = GetInstanceID();
        Messenger.Publish(new StartGuiDestilationMessage(_activationType, stats,idMessage));
    }

    public void Handle(EndGuiDestilationMessage message)
    {
        if (message.MessageId != idMessage) return;
        if (message.ModifiedStats.RedHearts != 0 || message.ModifiedStats.BlueHearts != 0 ||
            message.ModifiedStats.YellowHearts != 0)
        {
              Debug.Log("processing...");  
        }
        Messenger.Publish(new ContinueMessage());
        idMessage = 0;
    }
}

