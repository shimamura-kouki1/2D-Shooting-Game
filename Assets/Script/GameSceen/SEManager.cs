using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _bgm;
    private AudioSource _audioSource;
    private int _unmber = 0;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.resource = _bgm[_unmber];
    }

    public void DeathSE()
    {
        _unmber = 0;
        _audioSource.resource = _bgm[0];
        _audioSource.Play();
    }
    public void ShootSE()
    {
        _unmber = 1;
        _audioSource.resource= _bgm[1];
        _audioSource.Play();
    }
    public void EnemySpawn()
    {
        _unmber = 2;
        _audioSource.resource=_bgm[2];
        _audioSource.Play();
    }
    public void GameStart()
    {
        if(GameManeger.Instance.CurrentState == GameState.Title)
        _unmber = 3;
        _audioSource.resource = _bgm[3];
        _audioSource.Play();
    }
    public void PoseSE()
    {
        _unmber = 4;
        _audioSource.resource = _bgm[4];
        _audioSource.Play();
    }
}
