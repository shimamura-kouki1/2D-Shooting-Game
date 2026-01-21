using UnityEngine;
using UnityEngine.InputSystem;

public class UIPauseInput : MonoBehaviour
{
    [SerializeField] SEManager _seManager;
    private PlayerInput _playerInput;

    private InputAction _navigate;
    private InputAction _submit;
    private InputAction _pause;

    GameState _gameState;

    [SerializeField] private PauseMenu _pauseMenu;
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        _navigate = _playerInput.actions["navigate"];
        _submit = _playerInput.actions["submit"];
        _pause = _playerInput.actions["pause"];
    }

    void Update()
    {
        _gameState = GameManager.Instance.CurrentState;
        if (_pause.WasPressedThisFrame())
        {
            if (_gameState == GameState.Playing)
            {
                GameManager.Instance.SetState(GameState.Pause);//状態をポーズ状態に変更
                _seManager.PoseSE();
            }
            else if (_gameState == GameState.Pause)//ポーズ画面から変更
            {
                GameManager.Instance.SetState(GameState.Playing);
            }
        }

        if (_gameState != GameState.Pause)
            return;

        // 上下入力
        if (_navigate.WasPressedThisFrame())
        {
            Vector2 dir = _navigate.ReadValue<Vector2>();
            _pauseMenu.Navigate(dir);
        }

        // 決定
        if (_submit.WasPressedThisFrame())
        {
            _pauseMenu.Submit();
        }
    }
}
