using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// ゲームの状態決定の変更
/// </summary>
public class GameManeger : MonoBehaviour
{
    public static GameManeger Instance;//どこからでもアクセス可能

    public static event Action<GameState> OnStateChanged;

    public GameState CurrentState { get; private set; }//読み取り専用

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetState(GameState.Title);//ゲーム状態の初期化タイトルへ
    }
    /// <summary>
    /// プレイヤーの死亡モーション
    /// 状態変化
    /// </summary>
    public void PlayerDead()
    {
        SetState(GameState.GameOver);
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSecondsRealtime(1f);//ゲーム停止中でも動くようにRealtime
        SetState(GameState.Title);
    }
    /// <summary>
    /// ゲーム状態の決定する
    /// </summary>
    /// <param name="state"></param>
    public void SetState(GameState state)//ゲーム状態の決定
    {
        if(CurrentState == state)
            return;
        CurrentState = state;

        OnStateChanged?.Invoke(state);//変化の通知

        if (state == GameState.Title)
        {
            ResettableRegistry.ResetAll(); //初期化ゲーム全体を
        }

        Time.timeScale = (state == GameState.Playing) ? 1f : 0f;//Playing以外0に
    }
}
