using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 3f;
    [SerializeField] private float _width;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }
    void Update()
    {
        if (GameManeger.Instance.CurrentState == GameState.Title)
        {
            ResetPosition();
        }

        if (GameManeger.Instance.CurrentState != GameState.Playing) return;
        transform.Translate(Vector3.left * _scrollSpeed * Time.deltaTime);

        if (transform.position.x <= -_width)// ”wŒi‚Ì’†SˆÊ’u‚ª”wŒi‚Ì‰¡••ª¶‚És‚Á‚½‚ç
        {
            transform.position += Vector3.right * _width * 2f * Time.timeScale;//‰E•ûŒü‚É“ñ–‡•ªˆÚ“®
        }

    }
    private void ResetPosition()
    {
        transform.position = _startPosition;
    }

}
