using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCon : MonoBehaviour,IResettable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _Player;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float miniX;
    [SerializeField] private float miniY;
    private InputAction _move;
    private PlayerInput _playerInput;
    private Vector2 _horizontar;

    private Transform _tr;

    private Vector3 _initialPosition;

    [SerializeField] Spowaner _spowaner;

    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Sprite[] _idleSprites;
    private int _index = 0;

    [SerializeField] PlayerHit _playerHit;
    [SerializeField] SEManager _seManager;

    void Awake()
    {
        _tr = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();
        _move = _playerInput.actions["move"];
        Application.targetFrameRate = 60;
        ResettableRegistry.Register(this);
    }
    void Start()
    {
        SaveInitialState();
    }

    void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
    }

    void Update()
    {
        if (GameManeger.Instance.CurrentState != GameState.Playing)
            return;
        if (_playerHit.IsDead)
            return;

        if (_playerInput.actions["fire"].IsPressed())
        {
            if (Time.frameCount % 30 == 0)
            {
                _seManager.ShootSE();//’e”­ŽËŽž‚ÌSE
                _spowaner.FireBullet();
            }
        }
        if (_move.IsPressed())
        {
            _horizontar = _move.ReadValue<Vector2>();
            float Y = _tr.position.y + _horizontar.y * _moveSpeed * Time.deltaTime;
            float X = _tr.position.x +_horizontar.x * _moveSpeed * Time.deltaTime;
            _tr.position = new Vector3(Mathf.Clamp( X, miniX, maxX),
                                       Mathf.Clamp(Y, miniY, maxY),
                                       0f);
        }
        IdleMotion();
    }
    public void SaveInitialState()
    {
        _initialPosition = _tr.position;
    }

    public void ResetToInitialState()
    {
        gameObject.SetActive(true);
        _tr.position = _initialPosition;
    }

    public void IdleMotion()
    {
        if (!_move.IsPressed())
        {
            _index = (_index+1) % _idleSprites.Length;
            _renderer.sprite = _idleSprites[_index];
        }
    }
}
