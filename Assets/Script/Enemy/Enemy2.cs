using UnityEngine;

public class Enemy2 : MonoBehaviour, IHittable
{

    [Header("Move")]
    [SerializeField] private float _enemySpeed = 5f; // 左へ移動する速さ


    [Header("Wave Move")]
    [SerializeField] private float _waveAmplitude = 1f; // 波の上下の大きさ
    [SerializeField] private float _waveFrequency = 2f; // 波の速さ


    [Header("Spawn / Out Position (World X)")]
    [SerializeField] private float _spawnX = 10f;   // 出現するX座標（画面右外）
    [SerializeField] private float _outX = -10f;    // 消えるX座標（画面左外）
    [SerializeField] private Vector2 _spawnYRange = new Vector2(-4f, 4f); // 出現Y範囲

    [SerializeField] private GameObject _hitEffect;

    [Header("Death Animation")]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _deathSprites;
    [SerializeField] private float _deathFrameTime = 0.1f;

    private bool _isDead = false;
    private int _deathIndex = 0;
    private float _deathTimer = 0f;


    private float baseY;     // 波の中心Y
    private float time;      // sin波用時間
    private bool _wasActive; // 前フレームで active だったか
    private bool _useWave;
    private bool _setActive = false;
    private Vector3 _stratPos = new Vector3(30f, 30f, 30f);

    [SerializeField] public float _halfWidth = 0.5f;
    [SerializeField] public float _halfHeight = 0.5f;

    [SerializeField] private SEManager _seManager;

    private Transform _tr;

    private void OnEnable()
    {
        GameManager.OnStateChanged += Statechange;
    }

    private void OnDisable()
    {
        GameManager.OnStateChanged -= Statechange;
    }

    void Start()
    {
        gameObject.SetActive(false);

        _tr = transform;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Title)
        {
            gameObject.SetActive(false);
            _wasActive = false;
            _setActive = false;
            _tr.position = _stratPos;
            return;
        }
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        if (_isDead)
        {
            DeathAnimation();
            return; // ← 死亡中は他の処理を止める
        }
        if (!_setActive)
        {
            HitManager.Instance.RegisterEnemy(this); //HitManegerのリストに追加
            _setActive = true;
        }

        if (!_wasActive && gameObject.activeSelf)
        {
            OnActivated();
        }

        if (gameObject.activeSelf && !_isDead)
        {
            CheckOutOfScreen();

            if (_useWave)
                WaveMove();
            else
                Move();

        }
        _wasActive = gameObject.activeSelf;

    }

    /// <summary>
    /// スポーン処理
    /// </summary>
    private void OnActivated()
    {
        // 出現Y座標はをランダムで決める
        float y = Random.Range(_spawnYRange.x, _spawnYRange.y);

        // 指定したX座標にワープ
        _tr.position = new Vector3(_spawnX, y, 0f);

        _seManager.EnemySpawn();//スポーンSE

        // 波の初期化
        baseY = y;
        time = 0f;

        _useWave = Random.Range(0, 2) == 0;
    }
    /// <summary>
    /// 垂直横移動
    /// </summary>
    private void Move()
    {
        time += Time.deltaTime;

        float x = _tr.position.x - _enemySpeed * Time.deltaTime;
        _tr.position = new Vector3(x, _tr.position.y, 0f);
    }
    /// <summary>
    /// ウェーブ移動
    /// </summary>
    private void WaveMove()
    {
        time += Time.deltaTime;

        float x = _tr.position.x - _enemySpeed * Time.deltaTime;
        float y = baseY + Mathf.Sin(time * _waveFrequency) * _waveAmplitude;

        _tr.position = new Vector3(x, y, 0f);
    }
    /// <summary>
    /// 画面外に出たら消す
    /// </summary>
    private void CheckOutOfScreen()
    {
        // 指定したX座標より左に行ったら消す
        if (_tr.position.x < _outX)
        {
            gameObject.SetActive(false);
            _tr.position = _stratPos;
            _setActive = false;
        }
    }
    /// <summary>
    /// 当たった時の処理
    /// </summary>
    /// <param name="bullet"></param>
    public void OnHit(Bullet bullet)
    {
        if (_deathSprites == null || _deathSprites.Length == 0)
        {
            Debug.LogError("Enemy2: DeathSprites が設定されていません", this);
            ResetEnemy();
            return;
        }
        // ヒットエフェクト
        HitEffectAuto effect = Instantiate(_hitEffect).GetComponent<HitEffectAuto>();
        effect.Play(_tr.position);

        //死亡SE
        _seManager.DeathSE();

        // 死亡演出開始
        _isDead = true;
        _deathIndex = 0;
        _deathTimer = 0f;
        _renderer.sprite = _deathSprites[0];

    }

    private void DeathAnimation()
    {
        _deathTimer += Time.deltaTime;

        if (_deathTimer >= _deathFrameTime)
        {
            _deathTimer = 0f;
            _deathIndex++;

            if (_deathIndex >= _deathSprites.Length)
            {
                // アニメーション終了
                ResetEnemy();
                return;
            }

            _renderer.sprite = _deathSprites[_deathIndex];
        }
    }
    /// <summary>
    /// 再利用可能の状態に初期化
    /// </summary>
    private void ResetEnemy()
    {
        _isDead = false;
        _setActive = false;
        _wasActive = false;
        _deathIndex = 0;
        _deathTimer = 0f;

        _tr.position = _stratPos;
        gameObject.SetActive(false);
    }

    private void Statechange(GameState state)
    {
        if (state == GameState.Title)
        {
            ResetEnemy();
        }
    }
}
