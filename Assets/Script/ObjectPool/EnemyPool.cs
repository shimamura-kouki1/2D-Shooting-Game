using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _poolsize;
    private readonly Queue<GameObject> _enemyPool;

    void Start()
    {
        InstantiateEnemyPoll();
    }
    void Update()
    {

    }
    private void InstantiateEnemyPoll()//ê∂ê¨äiî[
    {
        for (int i = 0; i < _poolsize; i++)
        {
            GameObject enemy = Instantiate(_enemy);//ê∂ê¨
            enemy.SetActive(false);//îÒÉAÉNÉeÉBÉu
            _enemyPool.Enqueue(enemy);//äiî[
        }
    }
    private GameObject GetEnemy()
    {
        GameObject getEnemy;

        if (_enemyPool.Count < 0f)
        {
            getEnemy = _enemyPool.Dequeue();
        }
        else
        {
            getEnemy = Instantiate(_enemy);
        }
        getEnemy.SetActive(true);

        var enemyComponent = getEnemy.GetComponent<Enemey>();
        enemyComponent._enemyPool = this;
        return getEnemy;
    }

    public void EnemyReturn()
    {
        _enemy.SetActive(false);
        _enemyPool.Enqueue(_enemy);
    }
}
