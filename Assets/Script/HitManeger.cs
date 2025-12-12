using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class HitManeger : MonoBehaviour
{
    public static HitManeger Instance;

    public List<bullet> _bullet = new List<bullet>();
    public List<Enemy> _enemy = new List<Enemy>();

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
        foreach (var bullet in _bullet)
        {
            Vector2 bulletPos = bullet.transform.position;

            foreach (var enemy in _enemy)
            {
                Vector2 enemyPos = enemy.transform.position;

                Vector2 distance = bulletPos - enemyPos;

                bool HitDistance = Mathf.Abs(distance.x) < enemy._halfWidth &&
                                   Mathf.Abs(distance.y) < enemy._halfHeight;

                if(HitDistance)
                {
                    bullet.ReturnPool();
                    break;
                }
            }

        }

    }
}
