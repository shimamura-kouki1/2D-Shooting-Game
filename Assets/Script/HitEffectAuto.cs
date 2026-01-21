using UnityEngine;

/// <summary>
/// エネミーのヒットエフェクト
/// </summary>
public class HitEffectAuto : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Play(Vector3 pos)
    {
        transform.position = pos;
        _particleSystem.Play();
        Destroy(gameObject, 1f);
    }
    //デストロイしてるから、再利用できる形にしたい
   
}
