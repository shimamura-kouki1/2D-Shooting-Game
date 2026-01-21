using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 3f;//スクロールのスピード
    [SerializeField] private float _width;//背景の横幅
    private Vector3 _startPosition;//スタート位置

    private Transform _tr;

    private void Start()
    {
        _tr = GetComponent<Transform>();
        _startPosition = _tr.position;
    }
        
    void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Title)
        {
            ResetPosition();
        }

        if (GameManager.Instance.CurrentState != GameState.Playing) return;
        _tr.Translate(Vector3.left * _scrollSpeed * Time.deltaTime);

        if (_tr.position.x <= -_width)// 背景の中心位置が背景の横幅分左に行ったら
        {
            _tr.position += Vector3.right * _width * 2f * Time.timeScale;//右方向に二枚分移動
        }

    }
    private void ResetPosition()
    {
        _tr.position = _startPosition;
    }

}
