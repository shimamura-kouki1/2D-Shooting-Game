using Unity.VisualScripting;
using UnityEngine;

public class HitEffectAuto : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    //void OnEnable()
    //{
    //    _particleSystem.Play();
    //    Invoke(nameof(Disable), _particleSystem.main.duration + _particleSystem.main.startLifetime.constantMax);
    //    Debug.Log("deta");
    //}

    void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Play(Vector3 pos)
    {
        Debug.Log($"HitEffect Play �Ă΂ꂽ pos={pos}");

        Debug.Log("aaa");
        transform.position = pos;
        _particleSystem.Play();
        Destroy(gameObject, 1f);
    }

   
}
