using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyPool _enemyPool { get; set; }

    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyDistance;

    [SerializeField] public float _halfWidth = 0.5f;
    [SerializeField] public float _halfHeight = 0.5f;

    private Transform _tr;

    void Start()
    {
        _tr = GetComponent<Transform>();
    }
}
