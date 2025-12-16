using UnityEngine;
using UnityEngine.EventSystems;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectButton;
    private void OnEnable()
    {
        // タイトル画面が表示された瞬間に
        // コントローラーの選択先を設定する
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }
    public void OnStartButton()
    {
        GameManeger.Instance.SetState(GameState.Playing);
    }

}
