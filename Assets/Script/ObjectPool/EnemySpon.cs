using UnityEngine;

public class EnemySpon : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;

    [SerializeField] private Transform _enemyPos;
    public void FireEnemy()
    {
        var enemy = _enemyPool.GetEnemy();
        enemy.transform.position = _enemyPos.transform.position;
    }
}
