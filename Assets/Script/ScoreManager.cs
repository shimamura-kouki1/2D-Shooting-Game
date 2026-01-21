using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    void Start()
    {
        _score = 0f;
        _textMeshProUGUI.text = _score.ToString();
    }
    public void Score()
    {
        _score += 100f;
        _textMeshProUGUI.text = _score.ToString();
    }

}
