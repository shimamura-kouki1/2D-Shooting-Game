using TMPro;
using UnityEngine;


public class Boss : MonoBehaviour, IHittable, IResettable
{
    [Header("ˆÚ“®")]
    [SerializeField] private float _enemyMove = 2f;
    private int _direction = 1;

    [Header("ˆÚ“®§ŒÀ")]
    [SerializeField] private float maxY;
    [SerializeField] private float miniY;


    private Transform _tr;
    private bool _isStopping;

    private void OnEnable()
    {
        ResettableRegistry.Register(this);
    }
    private void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
    }
    void Start()
    {
        SaveInitialState();
        _tr = transform;
    }

    void Update()
    {
            //‚±‚±‚Éƒ{ƒX‚ÌˆÚ“®ˆ—‚ð‘‚­
            float Y = _tr.position.y + _enemyMove * _direction * Time.deltaTime;

        if (Y >= maxY)
        {
            Y = maxY;
            _direction = -1;
        }
        if(Y <= miniY)
        {
            Y = miniY;
            _direction = 1;
        }

        _tr.position = new Vector3(_tr.position.x,Y,0f);

        EnemyEvents.OnEnemyFire?.Invoke();
    }

    public void OnHit(Bullet bullet)
    {
        throw new System.NotImplementedException();
    }

    public void SaveInitialState()
    {

    }
    public void ResetToInitialState()
    {
       
    }
}
