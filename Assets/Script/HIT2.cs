using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HIT2 : MonoBehaviour
{
    [SerializeField] public List<GameObject> _enemyList;
    [SerializeField] private float hitRadius = 1f;
    private List<Transform> _enemyPos;

    
    void Update()
    {
        foreach (GameObject enemy in _enemyList)
        {
            //(enemy.transform.position);
        }

        //if (enemy == null) continue;   //壊れたオブジェクトをスキップ

        //float dist = Vector2.Distance(this.transform.position, enemy.transform.position);

        //if (dist <= hitRadius)
        //{
        //    Debug.Log("aaa");
        //}
    }
}

