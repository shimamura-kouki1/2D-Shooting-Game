using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour,IResettable
{
    [Header("Move")]
    [SerializeField] private float _enemySpeed = 5f;

    [Header("Wave Move")]
    [SerializeField] private float _waveAmplitude = 1f;
    [SerializeField] private float _waveFrequency = 2f;
    [SerializeField, Range(0f, 1f)]

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

   
    private void Update()
    {
        if (GameManeger.Instance.CurrentState != GameState.Playing)
            return;
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
