using UnityEngine;
using System;

public static class  PlayerEvents
{
    /// <summary>
    /// スプリントした瞬間
    /// </summary>
    public static Action OnSprint;
    //Actionは通知だけ。スプリントしたかどうか
    /// <summary>
    /// MPが足りるか判定
    /// </summary>
    public static Func<bool> OnTryUsingMagic;
    //Funcは問い合わせができる。つまり、Boolとかでtureだったら実行みたいにできる
    public static Action<float, float> OnMpChanged;
}
