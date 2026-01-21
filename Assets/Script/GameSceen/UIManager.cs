using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject _titleUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _playUI;
    [SerializeField] private GameObject _gameOvare;
    private GameState _presentState;

    private void Start()
    {
        _presentState = GameManeger.Instance.CurrentState;//最初にタイトルを出すために
        UpdateUI(_presentState);//UI更新
    }
    void Update()
    {
        GameState gameState = GameManeger.Instance.CurrentState;
        if (_presentState == gameState) return;//ゲームの状態が一緒だったらリターン

        _presentState = gameState;
        UpdateUI(gameState);
    }
    //ゲームの状態変更を実行
    private void UpdateUI(GameState gameState)
    {
        _titleUI.SetActive(gameState == GameState.Title);
        _pauseUI.SetActive(gameState == GameState.Pause);
        _playUI.SetActive(gameState == GameState.Playing);
        _gameOvare.SetActive(gameState == GameState.GameOver);
    }
}
