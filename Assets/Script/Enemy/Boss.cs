using System;
using System.Buffers.Text;
using TMPro;
using UnityEngine;


public class Boss : MonoBehaviour, IHittable, IResettable
{
    public event Action OnBossDeath;

    [Header("移動")]
    [SerializeField] private float _enemyMove = 2f;
    private int _direction = 1;

    [Header("移動制限")]
    [SerializeField] private float maxY;
    [SerializeField] private float miniY;

    [Header("攻撃")]
    [SerializeField] private EnemyBulletPool _bulletPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private PlayerHit _player;
    [SerializeField] private float _fireInterval = 1.5f;
    private float _fireTimer;

    [Header("HP")]
    [SerializeField] private int _hp;
    private int _currentHitCount;

    [Header("当たり判定")]
    [SerializeField] public float _halfWidth = 1f;
    [SerializeField] public float _halfHeight = 1f;

    [SerializeField] private float _spawnX = 10f;   // 出現するX座標（画面右外）
    [SerializeField] private float _spawuY = 0;

    [Header("爆発エフェクト")]
    [SerializeField] private Sprite[] _explosion;
    private bool _isDeath = true;

    private Transform _tr;

    //初期状態の保存用
    private Vector3 _initialPos;
    private int _initialDirection;

    private void OnEnable()
    {
        ResettableRegistry.Register(this);
        HitManager.Instance._boss.Add(this);
    }
    private void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
        HitManager.Instance._boss.Remove(this);
    }
    private void Awake()
    {
        _tr = transform;
        
    }
    void Start()
    {
        gameObject.SetActive(false);
        SaveInitialState();
    }

    void Update()
    {
        if (!_isDeath) return;
        //ここにボスの移動処理を書く
        float Y = _tr.position.y + _enemyMove * _direction * Time.deltaTime;

        if (Y >= maxY)
        {
            Y = maxY;
            _direction = -1;
        }
        if (Y <= miniY)
        {
            Y = miniY;
            _direction = 1;
        }

        _tr.position = new Vector3(_tr.position.x, Y, 0f);

        _fireTimer += Time.deltaTime;
        if (_fireTimer >= _fireInterval)
        {
            _fireTimer = 0f;
            Shoot(_player);
        }
    }
    /// <summary>
    /// プレイヤーの弾が当たったときの処理
    /// </summary>
    /// <param name="bullet"></param>
    public void OnHit(Bullet bullet)
    {
        _currentHitCount++;
        Debug.Log(_currentHitCount);
        bullet.gameObject.SetActive(false);

        {
            Die();
        }
    }

    public void Die()
    {
        OnBossDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void Shoot(PlayerHit player)
    {
        GameObject bulletObj = _bulletPool.GetEnemyBullet();
        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();

        bullet.transform.position = _firePoint.position;
        bullet.Init(player.transform.position);
    }

    public void SaveInitialState()//初期化の保存
    {
        _initialPos = _tr.position;
        _initialDirection = _direction;
        _currentHitCount = 0;
    }
    public void ResetToInitialState()//初期化するもの
    {
        _tr.position = _initialPos;
        _direction = _initialDirection;
        _currentHitCount = 0;
        _fireTimer = 0f;

        gameObject.SetActive(false);
    }
}
