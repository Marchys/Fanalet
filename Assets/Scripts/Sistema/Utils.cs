using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class Utils
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

    public static int LimitToRange(
        this int value, int inclusiveMinimum, int inclusiveMaximum)
    {
        if (value < inclusiveMinimum) { return inclusiveMinimum; }
        if (value > inclusiveMaximum) { return inclusiveMaximum; }
        return value;
    }
}
