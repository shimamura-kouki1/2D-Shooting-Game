using System.Collections;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [Header("DeathAnimetion")]
    public bool IsDead => _isDeath;
    private SpriteRenderer _renderer;
    [SerializeField] Sprite[] _deathSprites;

    [SerializeField] SEManager _seManager;

    private float _playerHalfWidth = 0.5f;
    private float _PlayerHalfHeight = 0.5f;
    private Transform _tr;
    private bool _isDeath = false;

    private void Start()
    {
        _tr = GetComponent<Transform>();
        _renderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        Resuscitation();

        if (_isDeath)return;

        PlayerHitCheak();
    }
    private void PlayerHitCheak()
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
                _seManager.DeathSE();//死亡SE
                break;
            }
        }
    }
    private void Die()
    {
        if (_isDeath) return;
        _isDeath = true;

        StartCoroutine(DeadSlowMotion());

    }
    private void Resuscitation()
    {
        if(GameManeger.Instance.CurrentState == GameState.Titel)
        {
            _isDeath = false;
            Time.timeScale = 1f;
        }
    }

    private IEnumerator DeadSlowMotion()
    {
        float slowScale = 0.1f;
        float deathDuration = 1f;
        Time.timeScale = slowScale;
        Time.fixedDeltaTime = 0.02f * slowScale;
        float frameTime = deathDuration / _deathSprites.Length;//イメージの切り替え速度

        for (int i = 0; i < _deathSprites.Length; i++)
        {
            _renderer.sprite = _deathSprites[i];
            yield return new WaitForSecondsRealtime(frameTime);
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        gameObject.SetActive(false);

        GameManeger.Instance.PlayerDead();
    }
}
