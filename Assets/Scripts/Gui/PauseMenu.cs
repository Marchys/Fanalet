using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class PauseMenu : MonoBehaviourEx, IHandle<PauseToggleMessage>
{


    private bool _transitioning = false;
    private bool _stopped = false;


    public void GoMenu()
    {
        JocManager.Generar.Pause(false);
        JocManager.Generar.ActualStat = JocManager.JocStats.Menu;
    }

    public void Handle(PauseToggleMessage message)
    {
        _stopped = !_stopped;
        JocManager.Generar.Pause(_stopped);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(_stopped);
        }
    }
}
