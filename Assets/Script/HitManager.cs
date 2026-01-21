using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 弾とエネミーのヒット判定
/// </summary>
public class HitManager : MonoBehaviour, IResettable
{
    public static HitManager Instance;

    public List<Bullet> _bullet = new List<Bullet>();
    public List<Enemy2> _enemy = new List<Enemy2>();

    private Transform _tr;

    private void Awake()
    {
        Instance = this;
        ResettableRegistry.Register(this);//リセット対象に登録
        _tr = GetComponent<Transform>();
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
    private void HitCheck()
    {
        for (int i = _bullet.Count - 1; i >= 0; i--)//foreachだと途中でリストが変わるからエラーになる/変わっても後ろから検証してるから検証ミスが起きない
        {
            Bullet bullet = _bullet[i];
            Vector2 bulletPos = bullet.transform.position;

            for (int k = _enemy.Count - 1; k >= 0; k--)
            {
                Enemy2 enemy = _enemy[k];

                Vector2 enemyPos = enemy.transform.position;

                Vector2 distance = bulletPos - enemyPos;//距離で計算しとる

                bool HitDistance = Mathf.Abs(distance.x) < enemy._halfWidth &&
                                   Mathf.Abs(distance.y) < enemy._halfHeight;

                if (HitDistance)
                {
                    if (enemy.TryGetComponent<IHittable>(out var hittable))
                    {
                        hittable.OnHit(bullet);
                    }
                    bullet.ReturnPool();

                    break;
                }
            }
        }
    }
    /// <summary>
    /// 使用禁止
    /// </summary>
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
        _enemy.Clear();
        foreach (var enemy in FindObjectsOfType<Enemy2>())
        {
            if (enemy.gameObject.activeInHierarchy)
                _enemy.Add(enemy);
        }
    }
    /// <summary>
    /// HitManagerへの登録
    /// </summary>
    /// <param name="enemy"></param>
    public void RegisterEnemy(Enemy2 enemy)//
    {
        if (!_enemy.Contains(enemy))
            _enemy.Add(enemy);
    }
}