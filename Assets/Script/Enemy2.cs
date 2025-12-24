using NUnit.Framework.Internal;
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


    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameManeger.Instance.CurrentState == GameState.Titel)
        {
            gameObject.SetActive(false);
            _wasActive = false;
            _setActive = false;
            transform.position = _stratPos;
            return;
        }
        if (GameManeger.Instance.CurrentState != GameState.Playing)
            return;

        if (_isDead)
        {
            DeathAnimation();
            return; // ← 死亡中は他の処理を止める
        }
        if (!_setActive)
        {
            HitManeger.Instance._enemy.Add(this);
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


    private void OnActivated()
    {
        // 出現Y座標はをランダムで決める
        float y = Random.Range(_spawnYRange.x, _spawnYRange.y);

        // 指定したX座標にワープ
        transform.position = new Vector3(_spawnX, y, 0f);

        _seManager.EnemySpawn();//スポーンSE

        // 波の初期化
        baseY = y;
        time = 0f;

        _useWave = Random.Range(0, 2) == 0;
    }
    private void Move()
    {
        time += Time.deltaTime;

        float x = transform.position.x - _enemySpeed * Time.deltaTime;
        transform.position = new Vector3(x, transform.position.y, 0f);
    }

    private void WaveMove()
    {
        time += Time.deltaTime;

        float x = transform.position.x - _enemySpeed * Time.deltaTime;
        float y = baseY + Mathf.Sin(time * _waveFrequency) * _waveAmplitude;

        transform.position = new Vector3(x, y, 0f);
    }
    private void CheckOutOfScreen()
    {
        // 指定したX座標より左に行ったら消す
        if (transform.position.x < _outX)
        {
            gameObject.SetActive(false);
            transform.position = _stratPos;
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
        effect.Play(transform.position);

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
    private void ResetEnemy()
    {
        _isDead = false;
        _setActive = false;
        _wasActive = false;
        _deathIndex = 0;
        _deathTimer = 0f;

        transform.position = _stratPos;
        gameObject.SetActive(false);
    }
}
