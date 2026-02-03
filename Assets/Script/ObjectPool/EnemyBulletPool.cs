using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _EnemyBullet;
    [SerializeField] private int _poolSize;
    private readonly Queue<GameObject> _pool = new Queue<GameObject>();

    private void Start()
    {
        InstantiateBulletPool();
    }
    private void InstantiateBulletPool()//格納
    {
        for (int i = 0; i < _poolSize; i++)
        {
            _pool.Enqueue(CreateEnemyBullet());
        }
    }

    private GameObject CreateEnemyBullet()//生成　GameObjectの戻り値が必要だから return enemyをしている。
    {
        GameObject enemy = Instantiate(_EnemyBullet);
        enemy.SetActive(false);
        return enemy;
    }
    public GameObject GetEnemyBullet()//取り出し
    {
        GameObject enemyBulletGet = _pool.Count >0 ? _pool.Dequeue() : CreateEnemyBullet();


        enemyBulletGet.SetActive(true);

        var bulletComponent = enemyBulletGet.GetComponent<EnemyBullet>();
        bulletComponent._enemybulletPool = this;

        return enemyBulletGet;
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
