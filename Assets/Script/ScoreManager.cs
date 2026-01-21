using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] private TextMeshPro _textMeshPro;

    void Start()
    {
        _score = 0f;
    }
    public void Score()
    {
        _score += 100f;
        _textMeshPro .text = _score.ToString();
    }

}
