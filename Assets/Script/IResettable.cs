using UnityEngine;
/// <summary>
/// 初期化用objectのやつ
/// </summary>
public interface IResettable
{
    // ゲーム開始時の状態を保存
    void SaveInitialState();

    // タイトルに戻るときに呼ばれる
    void ResetToInitialState();
}
