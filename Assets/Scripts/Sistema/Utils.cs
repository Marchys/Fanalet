using System;
using Random = UnityEngine.Random;

public class Utils
{
    public static bool ChanceTrue(int chance)
    {
        var realChance = (float)chance/100;
        return Random.Range(0f, 1.0f) < realChance;
    }

    public static string[] Lines(string source)
    {
        char[] archDelim = new char[] {'º'};
        return source.Split(archDelim, StringSplitOptions.RemoveEmptyEntries);
    }
}
