using TMPro;
using UnityEngine;


public class Boss : MonoBehaviour, IHittable, IResettable
{
    [Header("ˆÚ“®")]
    [SerializeField] private float _enemyMove = 2f;
    private int _direction = 1;

    [Header("ˆÚ“®§ŒÀ")]
    [SerializeField] private float maxY;
    [SerializeField] private float miniY;

    [Header("UŒ‚")]
    [SerializeField] private EnemyBulletPool _bulletPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private PlayerHit _player;
    [SerializeField] private float _fireInterval = 1.5f;
    private float _fireTimer;


    private Transform _tr;
    private bool _isStopping;

    private void OnEnable()
    {
        ResettableRegistry.Register(this);
    }
    private void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
    }
    void Start()
    {
        SaveInitialState();
        _tr = transform;
    }

    void Update()
    {
            //‚±‚±‚Éƒ{ƒX‚ÌˆÚ“®ˆ—‚ð‘‚­
            float Y = _tr.position.y + _enemyMove * _direction * Time.deltaTime;

        if (Y >= maxY)
        {
            Y = maxY;
            _direction = -1;
        }
        if(Y <= miniY)
        {
            Y = miniY;
            _direction = 1;
        }

        _tr.position = new Vector3(_tr.position.x,Y,0f);

        _fireTimer += Time.deltaTime;
        if (_fireTimer >= _fireInterval)
        {
            _fireTimer = 0f;
            Shoot(_player);
        }
    }

    public void OnHit(Bullet bullet)
    {
        throw new System.NotImplementedException();
    }

    public void Shoot(PlayerHit player)
    {
        GameObject bulletObj = _bulletPool.GetEnemyBullet();
        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();

        bullet.transform.position = _firePoint.position;
        bullet.Init(player.transform.position);
    }

    public void SaveInitialState()
    {

    }
    public void ResetToInitialState()
    {
       
    }
}
