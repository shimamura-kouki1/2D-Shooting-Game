using UnityEngine;
using System.Collections.Generic;

public class HitManeger : MonoBehaviour,IResettable
{
    public static HitManeger Instance;

    public List<Bullet> _bullet = new List<Bullet>();
    public List<Enemy2> _enemy = new List<Enemy2>();

    private Transform _tr;

    private void Awake()
    {
        Instance = this;
        ResettableRegistry.Register(this);
    }
    void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
    }
    void Update()
    {
        if (GameManeger.Instance.CurrentState == GameState.Titel)
        {
            ListReset();
        }

        if (GameManeger.Instance.CurrentState != GameState.Playing)
            return;
        HitChek();
    }
    private void HitChek()
    {
        for(int i = _bullet.Count - 1; i >= 0;i--)//foreachだと途中でリストが変わるからエラーになる/変わっても後ろから検証してるから検証ミスが起きない
            {
            Bullet bullet = _bullet[i];
            Vector2 bulletPos = bullet.transform.position;

            for (int k = _enemy.Count - 1; k >= 0; k--)
            {
                Enemy2 enemy = _enemy[k];

                //if (!enemy.gameObject.activeInHierarchy)
                //    continue;

                Vector2 enemyPos = enemy.transform.position;

                Vector2 distance = bulletPos - enemyPos;//距離で計算しとる

                bool HitDistance = Mathf.Abs(distance.x) < enemy._halfWidth &&
                                   Mathf.Abs(distance.y) < enemy._halfHeight;

                if (HitDistance)
                {
                    if (enemy.TryGetComponent<IHittable>(out var hittable))
                    {
                        hittable.OnHit(bullet);
                    }
                    bullet.ReturnPool();



                    //_bullet.RemoveAt(i);
                    break;
                }
            }

        }

    }

    public void ListReset()//Listのリセット
    {
        _bullet.Clear();
        _enemy.Clear();
    }


    public void SaveInitialState()
    {
        return;
    }
    public void ResetToInitialState()
    {
        _bullet.Clear();
        _enemy.Clear();
        foreach (var enemy in FindObjectsOfType<Enemy2>())
        {
            if (enemy.gameObject.activeInHierarchy)
                _enemy.Add(enemy);
        }
    }
}
