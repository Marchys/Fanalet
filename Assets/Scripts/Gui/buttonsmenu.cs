using UnityEngine;

public class buttonsmenu : MonoBehaviour {

    public void StartGame()
    {
        JocManager.Generar.ActualStat = JocManager.JocStats.Isla;
    }

    public void ExitGame()
    {
      Application.Quit();
    }
    
}
