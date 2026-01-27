using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour,IResettable
{
    [SerializeField] private float _score;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI _titleTextMeshProUGUI;

    private float _resetScore;

    private void OnEnable()
    {
        ResettableRegistry.Register(this);
    }
    private void OnDestroy()
    {
        ResettableRegistry.Unregister(this);
    }
    public void SaveInitialState()
    {
        _resetScore = 0f;
    }
    public void ResetToInitialState()
    {
        _score = _resetScore;
        _textMeshProUGUI.text = _score.ToString();
    }
    void Start()
    {
        _score = 0f;
        _textMeshProUGUI.text = _score.ToString();
        _titleTextMeshProUGUI.text = _score.ToString();
        
    }
    public void Score()
    {
        _score += 100f;
        _textMeshProUGUI.text = _score.ToString();
    }
    /// <summary>
    /// ハイスコアを保存する
    /// </summary>
    public void SaveHightScore()
    {
        float hightScore = PlayerPrefs.GetFloat("HightScore", 0f);

        if(_score > hightScore)
        {
            PlayerPrefs.SetFloat("HightScore", _score);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// ハイスコアをタイトルに表示
    /// </summary>
    public void TitleScore()
    {
        float hightScore = PlayerPrefs.GetFloat("HightScore", 0f);
        _titleTextMeshProUGUI.text = hightScore.ToString();
    }
}
