using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHitManager : MonoBehaviour, IHitSystem, IResettable
{
    public static EnemyBulletHitManager Instance;

    [SerializeField] public List<EnemyBullet> _bullet = new();
    [SerializeField] private PlayerHit _player;

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
        if (!_bullet.Contains(bullet))
            _bullet.Add(bullet);
    }

    public void UnregisterBullet(EnemyBullet bullet)
    {
        _bullet.Remove(bullet);
    }

    public void HitCheck()
    {
        if (_player == null) return;

        Vector2 playerPos = _player.transform.position;

        for (int i = _bullet.Count - 1; i >= 0; i--)//foreachだと途中でリストが変わるからエラーになる/変わっても後ろから検証してるから検証ミスが起きない
        {
            EnemyBullet bullet = _bullet[i];
            Vector2 bulletPos = bullet.transform.position;

            Vector2 distance = bulletPos - playerPos;//距離で計算しとる

            bool HitDistance = Mathf.Abs(distance.x) < _player._playerHalfWidth
                            && Mathf.Abs(distance.y) < _player._PlayerHalfHeight;

            if (HitDistance)
            {
                _player.Die();
                //bullet.ReturnPool();

                break;
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
        _bullet.Clear();
    }
}


