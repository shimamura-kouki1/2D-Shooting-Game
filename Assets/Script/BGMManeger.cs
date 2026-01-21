using UnityEngine;

public class BGMManeger : MonoBehaviour
{
    [SerializeField] private AudioClip[] _bgm;//BGMのリスト
    private AudioSource _audioSource;
    private int _unmber = 0;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.resource = _bgm[_unmber];
        _audioSource.Play();
    }

    private void Update()
    {
        BGMChanger();
    }

    /// <summary>
    /// BGMをシーンごとに変化
    /// </summary>
    private void BGMChanger()
    {
        //Titelかつ、要素が0以外なら実行
        if (GameManager.Instance.CurrentState == GameState.Title && _bgm[_unmber] != _bgm[0])
        {
            _unmber = 0;
            _audioSource.resource = _bgm[0];
            _audioSource.Play();
        }
        if (GameManager.Instance.CurrentState == GameState.Playing && _bgm[_unmber] != _bgm[1])
        {
            _unmber = 1;
            _audioSource.resource = _bgm[1];
            _audioSource.Play();
        }
        if (GameManager.Instance.CurrentState == GameState.GameOver && _bgm[_unmber] != _bgm[2])
        {
            _unmber = 2;
            _audioSource.resource = _bgm[2]; //(AudioClip)_bgm[2];
            _audioSource.Play();
        }
    }
}
