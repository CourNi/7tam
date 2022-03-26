using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        float tx = vector.x;
        float ty = vector.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }

    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }
}
