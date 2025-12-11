using UnityEngine;

public class Enemycre : MonoBehaviour
{
    [SerializeField] private EnemySpon _enemySpon;
    void Start()
    {
        _enemySpon.FireEnemy();
    }
}
