using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject _titleUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _playUI;
    [SerializeField] private GameObject _gameOvare;

    // Update is called once per frame
    void Update()
    {
        GameState gameState = GameState.Playing;

        _titleUI.SetActive(gameState == GameState.Titel);
        _pauseUI.SetActive(gameState == GameState.Pause);
        _playUI.SetActive(gameState == GameState.Playing);
        _gameOvare.SetActive(gameState == GameState.GameOver);
    }
}
