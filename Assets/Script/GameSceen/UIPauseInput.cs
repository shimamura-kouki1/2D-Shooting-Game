using UnityEngine;
using UnityEngine.InputSystem;

public class UIPauseInput : MonoBehaviour
{
    [SerializeField] SEManager _seManager;
    private PlayerInput _playerInput;

    private InputAction _navigate;
    private InputAction _submit;
    private InputAction _pause;

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
        if (_pause.WasPressedThisFrame())
        {
            if (GameManeger.Instance.CurrentState == GameState.Playing)
            {
                GameManeger.Instance.SetState(GameState.Pause);
                _seManager.PoseSE();
            }
            else if (GameManeger.Instance.CurrentState == GameState.Pause)
                GameManeger.Instance.SetState(GameState.Playing);
        }

        if (GameManeger.Instance.CurrentState != GameState.Pause)
            return;

        // ã‰º“ü—Í
        if (_navigate.WasPressedThisFrame())
        {
            Vector2 dir = _navigate.ReadValue<Vector2>();
            _pauseMenu.Navigate(dir);
        }

        // Œˆ’è
        if (_submit.WasPressedThisFrame())
        {
            _pauseMenu.Submit();
        }
    }
}
