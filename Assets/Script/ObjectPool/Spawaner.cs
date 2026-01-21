using UnityEngine;

public class Spawaner : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;

    [SerializeField] private Transform _playrePos;
    /// <summary>
    /// 弾を取得しプレイヤーの位置に出現
    /// </summary>
    public void FireBullet()
    {
        var bullet = _bulletPool.BulletGet();
        bullet.transform.position = _playrePos.transform.position;
    }
}
