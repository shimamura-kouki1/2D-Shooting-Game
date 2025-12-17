using UnityEngine;

public class Enemy2 : MonoBehaviour
{

    [Header("Move")]
    [SerializeField] private float _enemySpeed = 5f; // 左へ移動する速さ


    [Header("Wave Move")]
    [SerializeField] private float _waveAmplitude = 1f; // 波の上下の大きさ
    [SerializeField] private float _waveFrequency = 2f; // 波の速さ


    [Header("Spawn / Out Position (World X)")]
    [SerializeField] private float _spawnX = 10f;   // 出現するX座標（画面右外）
    [SerializeField] private float _outX = -10f;    // 消えるX座標（画面左外）
    [SerializeField] private Vector2 _spawnYRange = new Vector2(-4f, 4f); // 出現Y範囲


    private float baseY;     // 波の中心Y
    private float time;      // sin波用時間
    private bool _wasActive; // 前フレームで active だったか
    private bool _useWave;
    private bool _setActive = false;
    private Vector3 _stratPos = new Vector3(30f, 30f, 30f);

    [SerializeField] public float _halfWidth = 0.5f;
    [SerializeField] public float _halfHeight = 0.5f;


    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameManeger.Instance.CurrentState == GameState.Titel)
        {
            gameObject.SetActive(false);
            _wasActive = false;
            _setActive = false;
            transform.position = _stratPos;
            return;
        }
        if (GameManeger.Instance.CurrentState != GameState.Playing)
            return;

        if (!_setActive)
        {
            HitManeger.Instance._enemy.Add(this);
            _setActive = true;
        }

        if (!_wasActive && gameObject.activeSelf)
        {
            OnActivated();
        }

        if (gameObject.activeSelf)
        {
            CheckOutOfScreen();

            if (_useWave)
                WaveMove();
            else
                Move();

        }
        _wasActive = gameObject.activeSelf;
    }


    private void OnActivated()
    {
        // 出現Y座標はをランダムで決める
        float y = Random.Range(_spawnYRange.x, _spawnYRange.y);

        // 指定したX座標にワープ
        transform.position = new Vector3(_spawnX, y, 0f);

        // 波の初期化
        baseY = y;
        time = 0f;

        _useWave = Random.Range(0, 2) == 0; 
    }
    private void Move()
    {
        time += Time.deltaTime;

        float x = transform.position.x - _enemySpeed * Time.deltaTime;
        transform.position = new Vector3(x, transform.position.y, 0f);
    }

    private void WaveMove()
    {
        time += Time.deltaTime;

        float x = transform.position.x - _enemySpeed * Time.deltaTime;
        float y = baseY + Mathf.Sin(time * _waveFrequency) * _waveAmplitude;

        transform.position = new Vector3(x, y, 0f);
    }
    private void CheckOutOfScreen()
    {
        // 指定したX座標より左に行ったら消す
        if (transform.position.x < _outX)
        {
            gameObject.SetActive(false);
            transform.position = _stratPos;
            _setActive = false;
        }
    }
}
