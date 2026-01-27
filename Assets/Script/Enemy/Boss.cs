using TMPro;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class Boss : MonoBehaviour, IHittable, IResettable
{
    [SerializeField] private float _enemyMove = 5f;

    private float _moveInterval = 120;

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
        if(Time.frameCount % _moveInterval == 0)
        {
            //‚±‚±‚Éƒ{ƒX‚ÌˆÚ“®ˆ—‚ğ‘‚­
        }
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
