using UnityEngine;

public class Pebretero : ActionE  {

    public override void ExecuteAction(BaseCaracterStats stats)
    {
        base.ExecuteAction(stats);
        JocManager.Generar.ActualStat = JocManager.JocStats.Dungeon;
    }
}
