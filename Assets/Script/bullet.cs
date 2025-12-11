using UnityEngine;

public class bullet : MonoBehaviour
{
    public bulletPool _bulletPool { get; set; }

    [SerializeField] private float _bulletSpeesd;
    private Transform _transform;
    [SerializeField] private float _returnX;
    [SerializeField] private Vector3 _returnPos;


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
    }
}
