using UnityEngine;

public class Enemey : MonoBehaviour
{
    public EnemyPool _enemyPool { get; set; }

    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyDistance;

    private Transform _tr;

    void Start()
    {
        _tr = GetComponent<Transform>();
    }
    void Update()
    {
        
    }
    private void Hit()
    {
        
    }
}
