using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _poolsize;
    public Queue<GameObject> _enemyPool;

    public static EnemyPool instance;

    private void Awake()
    {
       _enemyPool = new Queue<GameObject>();
    }
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
            enemy.SetActive(true);//îÒÉAÉNÉeÉBÉu
            _enemyPool.Enqueue(enemy);//äiî[
        }
    }
    public GameObject GetEnemy()
    {
        GameObject getEnemy;

        if (_enemyPool.Count > 0f)
        {
            getEnemy = _enemyPool.Dequeue();
        }
        else
        {
            getEnemy = Instantiate(_enemy);
        }
        getEnemy.SetActive(true);

        var enemyComponent = getEnemy.GetComponent<Enemy>();
        enemyComponent._enemyPool = this;
        return getEnemy;
    }

    public void EnemyReturn()
    {
        _enemy.SetActive(false);
        _enemyPool.Enqueue(_enemy);
    }
}
