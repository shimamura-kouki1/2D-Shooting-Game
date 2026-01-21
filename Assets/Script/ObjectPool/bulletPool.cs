using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 弾のobjectプール
/// </summary>
public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _poolSize;
    private readonly Queue<GameObject> _pool = new Queue<GameObject>();


    private void Start()
    {
        InstantiatePool();
    }
    private void InstantiatePool()//格納
    {
        for (int i =0; i < _poolSize; i++)
        {
            _pool.Enqueue(CreateBullet());
        }
    }

    private GameObject CreateBullet()//生成　GameObjectの戻り値が必要だから return enemyをしている。
    {
        GameObject enemy = Instantiate(_bullet);
        enemy.SetActive(false);
        return enemy;
    }
    public GameObject BulletGet()//取り出し
    {
        GameObject bulletGet;
        if (_pool.Count > 0)
        {
            bulletGet = _pool.Dequeue();//取り出し
        }
        else
        {
            bulletGet = Instantiate(_bullet);//生成
        }

        bulletGet.SetActive(true);

        var bulletComponent = bulletGet.GetComponent<Bullet>();
        bulletComponent._bulletPool = this;

        return bulletGet;
    }

    /// <summary>
    /// 弾をオブジェクトプールへ回収
    /// </summary>
    /// <param name="bullet"></param>
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _pool.Enqueue(bullet);
    }
}
