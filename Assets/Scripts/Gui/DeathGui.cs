using UnityEngine;
using System.Collections;

public class DeathGui : MonoBehaviourEx, IHandle<PlayerDeathMessage>
{

    public void GoMenu()
    {
        JocManager.Generar.ActualStat = JocManager.JocStats.Menu;
    }

    public void Handle(PlayerDeathMessage message)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
