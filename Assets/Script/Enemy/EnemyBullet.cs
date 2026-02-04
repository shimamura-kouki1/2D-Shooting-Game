using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public EnemyBulletPool _enemybulletPool { get; set; }

    [SerializeField] private float _bulletSpeesd;
    private Transform _tr;

    [SerializeField] private float _maxReturnDistance = 10f;//‰ñŽû‚·‚é‹——£
    private Vector3 _spawnPos;//’e‚ªƒXƒ|[ƒ“‚·‚é‹——£

    [SerializeField] public float _halfWidth = 1f;
    [SerializeField] public float _halfHeight = 1f;

    private Vector3 _moveDir;

    private void OnEnable()
    {
        EnemyBulletHitManager.Instance._bullet.Add(this);
        BulletHitManager.Instance._enemyBullet.Add(this);
    }

    private void OnDisable()
    {
        if (EnemyBulletHitManager.Instance != null)
            EnemyBulletHitManager.Instance._bullet.Remove(this);
        if (BulletHitManager.Instance != null)
            BulletHitManager.Instance._enemyBullet.Remove(this);
    }

    private void Awake()
    {
        _tr = transform;
    }

    private void Update()
    {
        _tr.position += _moveDir * _bulletSpeesd * Time.deltaTime;

        if (Vector3.Distance(_spawnPos, _tr.position) > _maxReturnDistance ||
                GameManager.Instance.CurrentState == GameState.Title)
        {
            ReturnPool();
        }
    }
    /// <summary>
    /// ”­ŽËŽž‚É1‰ñ‚¾‚¯ŒÄ‚Ô
    /// </summary>
    public void Init(Vector3 playerPos)
    {
        _spawnPos = _tr.position;
        _moveDir = (playerPos - _tr.position).normalized;
    }

    public void ReturnPool()
    {
        _enemybulletPool.ReturnBullet(gameObject);
    }
}
