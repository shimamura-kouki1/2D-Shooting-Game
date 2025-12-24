using System.Collections;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger Instance;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetState(GameState.Titel);
    }

    public void PlayerDead()
    {
        SetState(GameState.GameOver);
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSecondsRealtime(1f);
        SetState(GameState.Titel);
    }

    public void SetState(GameState state)
    {
        CurrentState = state;

        if (state == GameState.Titel)
        {
            ResettableRegistry.ResetAll(); // Å© Ç±Ç±Ç†ÇÈÅH
        }

        Time.timeScale = (state == GameState.Playing) ? 1f : 0f;
    }
}
