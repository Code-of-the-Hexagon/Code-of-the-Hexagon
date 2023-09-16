using System;
using UnityEngine;

public static class Rng
{
    public static bool IsTrue(float chanceToBeTrue)
    {
        var num = UnityEngine.Random.Range(0f, 1f);
        var value = num < chanceToBeTrue;
        Debug.Log($"{num} {chanceToBeTrue}");
        return value;
    }

    public static float RandomFloat (float min, float max) =>
        UnityEngine.Random.Range(min, max);
}