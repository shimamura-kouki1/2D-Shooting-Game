using UnityEngine;
using UnityEngine.UI;

public class MPUI : MonoBehaviour
{
    [SerializeField] private Image _MPUI;

    private void OnEnable()
    {
        PlayerEvents.OnMpChanged += UpdateGauge;
    }

    private void OnDisable()
    {
        PlayerEvents.OnMpChanged -= UpdateGauge;
    }

    private void UpdateGauge(float current, float max)
    {
        _MPUI.fillAmount = current / max;
    }
}
