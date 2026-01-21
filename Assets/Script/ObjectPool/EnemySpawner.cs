using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy2[] _enemies;
    [SerializeField] private float _spawnInterval = 1f;//出現スピード

    private int _index = 0;
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()//一定間隔でエネミーを出現し続ける
    {
        while (true)
        {
            if (GameManager.Instance.CurrentState == GameState.Playing)
            {
                _enemies[_index].gameObject.SetActive(true);
                _index = (_index + 1) % _enemies.Length;

            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
