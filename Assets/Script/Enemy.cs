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

    public void OnDisable()
    {
        if (HitManeger.Instance != null)
        {
            HitManeger.Instance._enemy.Remove(this);
        }
    }

    void Start()
    {
        HitManeger.Instance._enemy.Add(this);
        _tr = GetComponent<Transform>();
    }
}
