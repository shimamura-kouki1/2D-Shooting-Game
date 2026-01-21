using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("選択するテキスト")]
    [SerializeField] private GameObject _resumeCursor;
    [SerializeField] private GameObject _titleCursor;

    [SerializeField] private SEManager _seManager;
    private int selectIndex = 0;
    private void OnEnable()
    {
        selectIndex = 0;
        UpdateVisual();
    }

    public void Navigate(Vector2 dir)//左右入力で入力の移動
    {
        if (dir.x > 0)
            selectIndex++ ;
        else if (dir.x < 0)
            selectIndex--;
        selectIndex = Mathf.Clamp(selectIndex, 0, 1);//範囲の限定
        UpdateVisual();
    }

    public void Submit()//入力の決定
    {
        if (selectIndex == 0)
            GameManager.Instance.SetState(GameState.Playing);
        else
            GameManager.Instance.SetState(GameState.Title);
    }

    private void UpdateVisual()//選択中の見た目の変化
    {
        _resumeCursor.SetActive(selectIndex == 0);
        _titleCursor.SetActive(selectIndex == 1);
    }
}
