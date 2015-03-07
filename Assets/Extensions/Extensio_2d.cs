using UnityEngine;

public static class Vector2Extension
{
 
    public static Vector2 Rotar(this Vector2 v, float graus) 
    {
        var sin = Mathf.Sin(graus * Mathf.Deg2Rad);
        var cos = Mathf.Cos(graus * Mathf.Deg2Rad);
 
        var tx = v.x;
        var ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}

