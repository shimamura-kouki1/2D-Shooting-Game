using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHitManager : MonoBehaviour, IHitSystem
{
    [SerializeField] public List<EnemyBullet> _bullet;
    [SerializeField] public PlayerHit _player;

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
}

