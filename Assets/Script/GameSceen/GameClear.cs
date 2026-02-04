using System.Collections;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnGameClear()
    {
        GameManager.Instance.SetState(GameState.GameOver);
        StartCoroutine(GamaClearSequence());
    }
    private IEnumerator GamaClearSequence()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameManager.Instance.SetState(GameState.Title);
    }
}
