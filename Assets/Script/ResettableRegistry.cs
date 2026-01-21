using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登録したオブジェクトの初期化
/// </summary>
public class ResettableRegistry : MonoBehaviour
{
    public static bool IsResetting { get; private set; }
    private static readonly List<IResettable> resettables = new();
    
    public static void Register(IResettable resettable)
    {
        if (!resettables.Contains(resettable))
            resettables.Add(resettable);
    }

    public static void Unregister(IResettable resettable)
    {
        resettables.Remove(resettable);
    }

    public static void ResetAll()
    {
        IsResetting = true;
        foreach (var r in resettables)
        {
            if (r is HitManager) continue;
            r.ResetToInitialState();
        }

        foreach (var r in resettables)
        {
            if (r is HitManager)
                r.ResetToInitialState();
        }
        IsResetting = false;
    }
}
