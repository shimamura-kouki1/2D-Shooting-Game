using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class MP : MonoBehaviour
{
    [SerializeField] private float _mp;
    [SerializeField] private float _mpHeelTime;
    [SerializeField] private float _sprintHeel;
    [SerializeField] private float _magicCost;
    [SerializeField] private float _maxMp;

    private void OnEnable()
    {
        PlayerEvents.OnSprint += SprintHeel;
        PlayerEvents.OnTryUsingMagic += TryFire;
    }

    private void OnDisable()
    {
        PlayerEvents.OnSprint -= SprintHeel;
        PlayerEvents.OnTryUsingMagic -= TryFire;
    }

    /// <summary>
    /// MPé©ìÆâÒïú
    /// </summary>
    private void Update()
    {
        _mp += _mpHeelTime * Time.deltaTime;
        _mp = Mathf.Min(_mp, _maxMp);
        UIMpChanged();
        Debug.Log(_mp);
    }
    /// <summary>
    /// èuä‘à⁄ìÆÇ≈MPâÒïú
    /// </summary>
    private void SprintHeel()
    {
        _mp += _sprintHeel;
        UIMpChanged();
    }

    private bool TryFire()
    {
        if (_mp<_magicCost) return false;

        _mp -= _magicCost;
        UIMpChanged();
        return true;
    }
    private void UIMpChanged()
    {
        PlayerEvents.OnMpChanged?.Invoke(_mp, _maxMp);
    }
}
