using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCon : MonoBehaviour,IResettable
{
    [Header("プレイヤーの設定")]
    [SerializeField] private float _moveSpeed;
    [SerializeField]private float _sprintMaxSpeed;
    private float _sprintSpeed;

    [Header("移動制限")]
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float miniX;
    [SerializeField] private float miniY;

    private InputAction _moveCon;
    private InputAction _sprintCon;
    private PlayerInput _playerInput;

    private Vector2 _horizontar;
    private Transform _tr;

    private Vector3 _initialPosition;//初期位置

    [Header("Sprict")]
    [SerializeField] Spawaner _spowaner;
    [SerializeField] PlayerHit _playerHit;
    [SerializeField] SEManager _seManager;

    [Header("Idle")]
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Sprite[] _idleSprites;
    private int _index = 0;

    private const string _fire = "fire";
    private const string _move = "move";
    private const string _sprint = "sprint";

    void Awake()
    {
        _tr = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();

        _moveCon = _playerInput.actions[_move];
        _sprintCon = _playerInput.actions[_sprint];

        ResettableRegistry.Register(this);//初期化登録

        Application.targetFrameRate = 60;
    }
    void Start()
    {
        _sprintSpeed = 1f;
        Application.targetFrameRate = 60;
        SaveInitialState();
    }

    void OnDestroy()
    {
        ResettableRegistry.Unregister(this);//初期化登録解除
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)return;
        
        if (_playerHit.IsDead)return;

        if (_playerInput.actions[_fire].IsPressed())
        {
            Fire();
        }

        if (_moveCon.IsPressed())
        {
            Sprint();
            _horizontar = _moveCon.ReadValue<Vector2>();
            float X = _tr.position.x + _horizontar.x * _moveSpeed * _sprintSpeed * Time.deltaTime;
            float Y = _tr.position.y + _horizontar.y * _moveSpeed * _sprintSpeed * Time.deltaTime;

            _tr.position = new Vector3(Mathf.Clamp(X, miniX, maxX),
                                       Mathf.Clamp(Y, miniY, maxY),
                                       0f);

            _sprintSpeed = 1f;
        }
        IdleMotion();
    }

    private void Fire()
    {
        if (Time.frameCount % 30 == 0)
        {
            _seManager.ShootSE();//弾発射時のSE
            _spowaner.FireBullet();
        }
    }

    /// <summary>
    /// 一瞬加速
    /// </summary>
    private void Sprint()
    {
        if (_sprintCon.WasPressedThisFrame())
        {
            _sprintSpeed = _sprintMaxSpeed;
        }
    }
    public void SaveInitialState()
    {
        _initialPosition = _tr.position;
    }



    /// <summary>
    /// ポジションリセット
    /// </summary>
    public void ResetToInitialState()
    {
        gameObject.SetActive(true);
        _tr.position = _initialPosition;
    }

    /// <summary>
    /// 待機時のアニメーション
    /// </summary>
    public void IdleMotion()
    {
        if (!_moveCon.IsPressed())
        {
            _index = (_index+1) % _idleSprites.Length;
            _renderer.sprite = _idleSprites[_index];
        }
    }
}
