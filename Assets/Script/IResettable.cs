using UnityEngine;
/// <summary>
/// 初期化用objectのやつ
/// </summary>
public interface IResettable
{
    /// <summary>
    /// ゲーム開始時の状態を保存
    /// </summary>
    void SaveInitialState();

    /// <summary>
    /// タイトルに戻るときに呼ばれる
    /// </summary>
    void ResetToInitialState();
}
