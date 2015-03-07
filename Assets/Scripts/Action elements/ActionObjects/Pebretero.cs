using UnityEngine;

public class Pebretero : ActionE  {

    public override void ExecuteAction()
    {
        JocManager.Generar.ActualStat = JocManager.JocStats.Dungeon;
    }
}
