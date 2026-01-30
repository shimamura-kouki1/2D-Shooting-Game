using UnityEngine;

public class BossFire : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    [SerializeField] private BulletPool _bulletPool;

    public void OnEnable()
    {
        EnemyEvents.OnEnemyFire += BoosFireAtPlayer;
    }

    public void OnDestroy()
    {
        EnemyEvents.OnEnemyFire -= BoosFireAtPlayer;
    }

    private void BoosFireAtPlayer()
    {
        Vector3 direction = (_playerPos.position - transform.position).normalized;
        var bullet = _bulletPool.BulletGet();
        bullet.transform.position = transform.position;


    }
}
