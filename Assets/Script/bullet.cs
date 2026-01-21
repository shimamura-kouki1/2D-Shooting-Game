using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPool _bulletPool { get; set; }


    [SerializeField] private float _bulletSpeesd;//’e‚Ì‘¬“x
    private Transform _tr;
    [SerializeField] private float _returnX;//ˆÚ“®ŒÀŠE
    [SerializeField] private Vector3 _returnPos;//‹A‚éˆÊ’u

    public void OnEnable()
    {
        HitManager.Instance._bullet.Add(this);//ƒŠƒXƒg‚É“o˜^
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
        _tr = GetComponent<Transform>();
        _returnPos = new Vector3(_returnX, 0f, 0f);
    }
    private void Update()
    {
        //ˆÚ“®ˆ—
        _tr.position += new Vector3(_tr.right.x + _bulletSpeesd, 0f, 0f) * Time.deltaTime;

        if (_tr.position.x > _returnPos.x)//ˆÚ“®ŒÀŠE‚Ü‚Å—ˆ‚½‚ç‰ñŽû
        {
            ReturnPool();
        }
        if (GameManager.Instance.CurrentState == GameState.Title)//ƒ^ƒCƒgƒ‹‚È‚ç‰ñŽû
        {
            ReturnPool();
        }
    }
    public void ReturnPool()
    {
        _bulletPool.ReturnBullet(gameObject);
    }
}
