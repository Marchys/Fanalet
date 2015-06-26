using UnityEngine;
using System.Collections;

public class DeathGui : MonoBehaviourEx, IHandle<PlayerDeathMessage>, IHandle<EndTransitionGuiMessage>
{
    private int _messageId;

    private void Start()
    {
        _messageId = GetInstanceID();
    }

    public void GoMenu()
    {
        JocManager.Generar.ActualStat = JocManager.JocStats.Menu;
    }

    public void Handle(PlayerDeathMessage message)
    {
        Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.NormalTransition, Constants.GuiTransitions.In, _messageId));
    }

    public void Handle(EndTransitionGuiMessage message)
    {
        if (message.MessageId != _messageId) return;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
