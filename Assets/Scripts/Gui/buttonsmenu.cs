using UnityEngine;

public class buttonsmenu : MonoBehaviour
{

    public GameObject HowtoplayDialog;
    
    public void StartGame()
    {
        JocManager.Generar.ActualStat = JocManager.JocStats.Isla;
    }

    public void ExitGame()
    {
      Application.Quit();
    }

    public void OpenHowToPlay()
    {
        foreach (Transform child in HowtoplayDialog.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void CloseHowToPlay()
    {
        foreach (Transform child in HowtoplayDialog.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    
}
