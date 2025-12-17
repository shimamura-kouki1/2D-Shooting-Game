using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private float _playerHalfWidth = 0.5f;
    private float _PlayerHalfHeight = 0.5f;
    private Transform _tr;
    private bool _isDeth = false;

    private void Start()
    {
        _tr = GetComponent<Transform>();
    }
    void Update()
    {
        Resuscitation();
        if (_isDeth) return;
        PlayerHitChek();
    }
    private void PlayerHitChek()
    {
        Vector2 PlayerPos = _tr.position;
        for (int i = HitManeger.Instance._enemy.Count - 1; i >= 0; i--)
        {
            Enemy2 enemy = HitManeger.Instance._enemy[i];
            Vector2 enemyPos = enemy.transform.position;

            Vector2 distance = enemyPos - PlayerPos;

            bool HitDistance = Mathf.Abs(distance.x) < (enemy._halfWidth + _playerHalfWidth) &&
                               Mathf.Abs(distance.y) < (enemy._halfHeight + _PlayerHalfHeight);

            if (HitDistance)
            {
                Die();

                break;
            }
        }
    }
    private void Die()
    {
        _isDeth = true;

        gameObject.SetActive(false);

        GameManeger.Instance.PlayerDead();
    }
    private void Resuscitation()
    {
        if(GameManeger.Instance.CurrentState == GameState.Titel)
        {
            _isDeth = false;
        }
    }
}
