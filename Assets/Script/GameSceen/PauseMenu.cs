using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _resumeCursor;
    [SerializeField] private GameObject _titleCursor;

    [SerializeField] private SEManager _seManager;
    private int selectIndex = 0;
    private void OnEnable()
    {
        selectIndex = 0;
        UpdateVisual();
    }

    public void Navigate(Vector2 dir)
    {
        if (dir.x > 0)
            selectIndex--;
        else if (dir.x < 0)
            selectIndex++;
        selectIndex = Mathf.Clamp(selectIndex, 0, 1);
        UpdateVisual();
    }

    public void Submit()
    {
        if (selectIndex == 0)
            GameManeger.Instance.SetState(GameState.Playing);
        else
            GameManeger.Instance.SetState(GameState.Titel);
    }

    private void UpdateVisual()
    {
        _resumeCursor.SetActive(selectIndex == 0);
        _titleCursor.SetActive(selectIndex == 1);
    }
}
