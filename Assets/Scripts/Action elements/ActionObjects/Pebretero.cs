using UnityEngine;

public class Pebretero : ActionE, IHandle<EndTransitionGuiMessage>
{
    private int _idMessage;

    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        base.ExecuteAction(stats);
        _idMessage = GetInstanceID();
        Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.In, _idMessage));
    }

    public void Handle(EndTransitionGuiMessage message)
    {
        if (message.MessageId != _idMessage) return;
        JocManager.Generar.ActualStat = JocManager.JocStats.Dungeon;
    }
}
