using System;
using UnityEngine;

public class RareEnemy : MonoBehaviour, IHittable, IResettable
{
    public event Action OnSpawneRareEnemy;

    [Header("à⁄ìÆ")]
    [SerializeField] private float _enemyMove = 2f;
    private int _direction = 1;
    [SerializeField] private Vector3 _spawnPos;

    [Header("à⁄ìÆêßå¿")]
    [SerializeField] private float maxY;
    [SerializeField] private float miniY;

    [Header("çUåÇ")]
    [SerializeField] private EnemyBulletPool _bulletPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private PlayerHit _player;
    [SerializeField] private float _fireInterval = 1.5f;
    private float _fireTimer;

    [Header("ìñÇΩÇËîªíË")]
    [SerializeField] public float _halfWidth = 1f;
    [SerializeField] public float _halfHeight = 1f;

    private Transform _tr;

    private Vector3 _initialPos;
    private int _initialDirection;
    private bool _isDeath = true;

    private Vector3 _stratPos = new Vector3(30f, 30f, 30f);
    private void OnEnable()
    {
        ResettableRegistry.Register(this);
        _tr = transform;

        if (HitManager.Instance != null)
            HitManager.Instance.OnRareEnemySpawn += Spawn;
    }
    private void OnDestroy()
    {
        ResettableRegistry.Unregister(this);

        if (HitManager.Instance != null)
            HitManager.Instance.OnRareEnemySpawn -= Spawn;
    }
    void Start()
    {
        gameObject.SetActive(false);
        SaveInitialState();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDeath) return;

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
    public void Spawn()
    {
        _isDeath = false;
        _tr.position = _spawnPos;
    }
    public void OnHit(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        Die();
    }
    public void Die()
    {
        SaveInitialState();
    }
    public void Shoot(PlayerHit player)
    {
        GameObject bulletObj = _bulletPool.GetEnemyBullet();
        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();

        bullet.transform.position = _firePoint.position;
        bullet.Init(player.transform.position);
    }
    public void SaveInitialState()//èâä˙âªÇÃï€ë∂
    {
        _initialPos = _tr.position;
        _initialDirection = _direction;
    }
    public void ResetToInitialState()//èâä˙âªÇ∑ÇÈÇ‡ÇÃ
    {
        _tr.position = _initialPos;
        _direction = _initialDirection;
        _fireTimer = 0f;
    }
}
