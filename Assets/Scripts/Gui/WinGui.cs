using UnityEngine;
using System.Collections;

public class WinGui : MonoBehaviourEx, IHandle<WinGameMessage>
{
    public void GoMenu()
    {
        JocManager.Generar.ActualStat = JocManager.JocStats.Menu;
    }

    public void Handle(WinGameMessage message)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
  
}
