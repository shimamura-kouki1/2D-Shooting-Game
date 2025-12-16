using System.Buffers.Text;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,IResettable
{
    [Header("Move")]
    [SerializeField] private float _enemySpeed = 5f;

    [Header("Wave Move")]
    [SerializeField] private float _waveAmplitude = 1f;
    [SerializeField] private float _waveFrequency = 2f;
    private float baseY;
    private float time;

    public EnemyPool _enemyPool { get; set; }

    [SerializeField] private float _enemyDistance;

    [SerializeField] public float _halfWidth = 0.5f;
    [SerializeField] public float _halfHeight = 0.5f;

    private Transform _tr;
    private Vector3 _initialPosition;
    private bool _initialActive;

    void Awake()
    {
        _tr = GetComponent<Transform>();
        SaveInitialState();                 
        ResettableRegistry.Register(this);
    }

    void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
    }
    void Start()
    {
        baseY = transform.position.y;
    }


    private void Update()
    {
        if (GameManeger.Instance.CurrentState != GameState.Playing)
            return;

        WaveMove();

    }

    private void WaveMove()
    {
        time += Time.deltaTime;

        float x = transform.position.x - _enemySpeed * Time.deltaTime;
        float y = baseY + Mathf.Sin(time * _waveFrequency) * _waveAmplitude;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void SaveInitialState()
    {
        _initialPosition = _tr.position;
        _initialActive = gameObject.activeSelf;
    }

    public void ResetToInitialState()
    {
        gameObject.SetActive(_initialActive); 
        _tr.position = _initialPosition;
    }
}
