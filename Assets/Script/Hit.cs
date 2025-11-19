using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Hit : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemyTarget;
    [SerializeField] private Vector2 _enemySize;

    private Transform _tr;

    private void Start()
    {
        _tr = GetComponent<Transform>();
    }
    void Update()
    {
        foreach(var enemyTarget in _enemyTarget)
        {
            Vector2 myPos = transform.position;
            Vector2 targetPos = enemyTarget.transform.position;

            if (Mathf.Abs(myPos.x - targetPos.x) <= (_enemySize.x / 2f + 0.5f) &&
                Mathf.Abs(myPos.y - targetPos.y) <= (_enemySize.y / 2f + 0.5f))
            {

            }
        }
    }
}