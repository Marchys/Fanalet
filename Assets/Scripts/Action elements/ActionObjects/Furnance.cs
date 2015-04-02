using UnityEngine;
using System.Collections;

public class Furnance : ActionE, IHandle<EndGuiDestilationMessage>, IHandle<EndTakeOil>, IHandle<ProtaEntersLighthouseMessage>, IHandle<ProtaExitsLighthouseMessage>
{

    private string _activationType;
    private int _idMessage = 0;
    private bool _destilating = false;
    private CountDown _countdown;

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
        _idMessage = GetInstanceID();
        if (!_destilating)
        {
            Messenger.Publish(new StartGuiDestilationMessage(_activationType, stats, _idMessage));
        }
        else
        {
            int oilDestilated;
            if(_countdown.isFinished) oilDestilated = (int)_countdown.life;
            else oilDestilated = (int) _countdown.elapsed;
            Messenger.Publish(new StartTakeOil(stats,_activationType, oilDestilated, _idMessage));
        }
       
    }
  

    private void StartDestilation(BaseCaracterStats modifiedStats)
    {
        float processTime;
        switch (_activationType)
        {

            case "red":
                processTime = -2 * modifiedStats.RedHearts;
                break;
            case "blue":
                processTime = -4 * modifiedStats.BlueHearts;
                break;
            case "yellow":
                processTime = -8 * modifiedStats.YellowHearts;
                break;
            default:
                processTime = 0;
                break;
        }
       
        _countdown = new CountDown(processTime);
        _countdown.Start();
        _countdown.PauseToggle();
        _destilating = true;
    }

    public void Handle(EndGuiDestilationMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (message.ModifiedStats.RedHearts != 0 || message.ModifiedStats.BlueHearts != 0 ||
            message.ModifiedStats.YellowHearts != 0)
        {
            StartDestilation(message.ModifiedStats);
        }
        Messenger.Publish(new ContinueMessage());
        _idMessage = 0;
    }

    public void Handle(EndTakeOil message)
    {
        if (message.MessageId != _idMessage) return;
        if (message.IsOilCollected)
        {
            _destilating = false;
        }
        Messenger.Publish(new ContinueMessage());
    }

    public void Handle(ProtaEntersLighthouseMessage message)
    {
        if (_destilating)
        {
            _countdown.PauseToggle();
        }
       
    }

    public void Handle(ProtaExitsLighthouseMessage message)
    {
        if (_destilating)
        {
            _countdown.PauseToggle();
        } 
    }
}

