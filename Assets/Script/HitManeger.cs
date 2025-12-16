using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class HitManeger : MonoBehaviour
{
    public static HitManeger Instance;

    public List<Bullet> _bullet = new List<Bullet>();
    public List<Enemy> _enemy = new List<Enemy>();

    private Transform _tr;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        HitChek();
    }
    private void HitChek()
    {
        for(int i = _bullet.Count - 1; i >= 0;i--)//foreachだと途中でリストが変わるからエラーになる/変わっても後ろから検証してるから検証ミスが起きない
            {
            Bullet bullet = _bullet[i];
            Vector2 bulletPos = bullet.transform.position;

            for (int k = _enemy.Count - 1; k >= 0; k--)
            {
                Enemy enemy = _enemy[k];
                Vector2 enemyPos = enemy.transform.position;

                Vector2 distance = bulletPos - enemyPos;//距離で計算しとる

                bool HitDistance = Mathf.Abs(distance.x) < enemy._halfWidth &&
                                   Mathf.Abs(distance.y) < enemy._halfHeight;

                if (HitDistance)
                {
                    bullet.ReturnPool();
                    enemy.gameObject.SetActive(false);
                    break;
                }
            }

        }

    }
}
