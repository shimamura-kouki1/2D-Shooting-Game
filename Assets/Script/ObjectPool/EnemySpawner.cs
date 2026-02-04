using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy2[] _enemies;
    [SerializeField] private float _spawnInterval = 1f;//出現スピード
    [SerializeField] private int _spawnCount = 29;
    [SerializeField] private Boss _boss;

    private int _index = 0;
    private bool _bossSpawned = false;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()//一定間隔でエネミーを出現し続ける
    {
        while (true)
        {
            if (HitManager.Instance._enemyCount >= _spawnCount)
            {
                if(!_bossSpawned)
                {
                    _bossSpawned = true;
                    _boss.gameObject.SetActive(true);
                }
                yield break; // Coroutineを終了
            }
            if (GameManager.Instance.CurrentState == GameState.Playing)
            {
                _enemies[_index].gameObject.SetActive(true);
                _index = (_index + 1) % _enemies.Length;

            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
