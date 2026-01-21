using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPool _bulletPool { get; set; }


    [SerializeField] private float _bulletSpeesd;
    private Transform _transform;
    [SerializeField] private float _returnX;
    [SerializeField] private Vector3 _returnPos;

    public void OnEnable()
    {
        HitManager.Instance._bullet.Add(this);
    }
    public void OnDisable()
    {
        if(HitManager.Instance != null)
        {
            HitManager.Instance._bullet.Remove(this);
        }
    }

    void Start()
    {
        _transform = GetComponent<Transform>();
        _returnPos = new Vector3(_returnX, 0f, 0f);
    }
    private void Update()
    {
        _transform.position += new Vector3(_transform.right.x + _bulletSpeesd, 0f, 0f) * Time.deltaTime;

        if (_transform.position.x > _returnPos.x)
        {
            _bulletPool.ReturnBullet(gameObject);
        }
        if (GameManeger.Instance.CurrentState == GameState.Title)
        {
            _bulletPool.ReturnBullet(gameObject);
        }
    }
    public void ReturnPool()
    {
        _bulletPool.ReturnBullet(gameObject);
    }
}
