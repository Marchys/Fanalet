using UnityEngine;

public static class Utils
{
    public static bool ChanceTrue(int chance)
    {
        var realChance = (float)chance/100;
        return Random.Range(0f, 1.0f) < realChance;
    }
}
