using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GameClearUI : MonoBehaviour
{
    public static GameClearUI Instance;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Boss _boss;

    private void Start()
    {
        if (_boss != null)
            _boss.OnBossDeath += OnBossDeth;
    }
    private void OnDestroy()
    {
        if (_boss != null)
            _boss.OnBossDeath -= OnBossDeth;
    }
    private void Awake()
    {
        Instance = this;
    }
    public void OnBossDeth()
    {
        Debug.Log("!!!");
        GameManager.Instance.SetState(GameState.GamneClear);
        _scoreManager.SaveHightScore();
        StartCoroutine(GamaClearSequence());
    }
    private IEnumerator GamaClearSequence()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameManager.Instance.SetState(GameState.Title);
    }
}
