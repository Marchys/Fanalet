using UnityEngine;

public class buttonsmenu : MonoBehaviour {

    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(280, 150, 140, 150), "Menu");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(300, 200, 100, 30), "Start"))
        {
            JocManager.Generar.ActualStat = JocManager.JocStats.Isla;
        }

        if (GUI.Button(new Rect(300, 250, 100, 30), "Exit"))
        {
            Application.Quit();
        }

    }
}
