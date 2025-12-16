using UnityEngine;

public class Spowaner : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;

    [SerializeField] private Transform _playrePos;
    public void FireBullet()
    {
        var bullet = _bulletPool.BulletGet();
        bullet.transform.position = _playrePos.transform.position;
    }
}
