using UnityEngine;

public class BossFire : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    [SerializeField] private EnemyBulletPool _enemybulletPool;

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
        Vector3 direction = (transform.position - _playerPos.position ).normalized;
        var bullet = _enemybulletPool.EnemyBulletGet();
        bullet.transform.position = transform.position;
    }
}
