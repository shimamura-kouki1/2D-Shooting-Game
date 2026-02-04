using System.Collections.Generic;
using UnityEngine;

public class BulletHitManager : MonoBehaviour, IResettable, IHitSystem
{
    public static BulletHitManager Instance;
    [SerializeField] public List<EnemyBullet> _enemyBullet = new();
    [SerializeField] public List<Bullet> _playerBullet = new();
    private void Awake()
    {
        Instance = this;
        ResettableRegistry.Register(this);//リセット対象に登録
    }
    void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
    }
    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;
        HitCheck();
    }
    public void RegisterBullet(EnemyBullet bullet)
    {
        if (!_enemyBullet.Contains(bullet))
            _enemyBullet.Add(bullet);
    }
    public void RegisterPlayerBullet(Bullet bullet)
    {
        if (!_playerBullet.Contains(bullet))
            _playerBullet.Add(bullet);
    }

    public void UnregisterBullet(EnemyBullet bullet)
    {
        _enemyBullet.Remove(bullet);
    }
    public void UnregisterPlayerBullet(Bullet bullet)
    {
        _playerBullet.Remove(bullet);
    }

    public void HitCheck()
    {
        Debug.Log("aaa");
        for (int i = _enemyBullet.Count - 1; i >= 0; i--)
        {
            EnemyBullet enemyBullet = _enemyBullet[i];
            Vector2 enemybulletPos = enemyBullet.transform.position;

            for (int k = _playerBullet.Count - 1; k >= 0; k--)
            {
                Bullet playerbullet = _playerBullet[k];
                Vector2 playerBulletPos = playerbullet.transform.position;

                Vector2 distance = enemybulletPos - playerBulletPos;

                bool HitDistance = Mathf.Abs(distance.x) < enemyBullet._halfWidth&&
                                   Mathf.Abs(distance.y) < enemyBullet._halfHeight;
                Debug.Log("eeee");
                if(HitDistance)
                {
                    enemyBullet.ReturnPool();
                    playerbullet.ReturnPool();
                }
            }
        }
    }

    public void SaveInitialState()
    {
        return;
    }
    /// <summary>
    /// タイトルに戻ったときの初期化
    /// </summary>
    public void ResetToInitialState()
    {
        _enemyBullet.Clear();
        _playerBullet.Clear();
    }
}
