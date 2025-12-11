using System.Collections;
using UnityEngine;

public class Enemey : MonoBehaviour
{
    public EnemyPool _enemyPool { get; set; }

    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyDistance;

    [SerializeField] private HIT2 _Aaa;

    private Transform _tr;

    void Start()
    {
        StartCoroutine(ListAdd());
        _tr = GetComponent<Transform>();
    }
    IEnumerator ListAdd()
    {
        yield return new WaitForSeconds(2);
        _Aaa._enemyList.Add(this.gameObject);
    }
}
