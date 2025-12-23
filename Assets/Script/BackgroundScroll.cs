using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 3f;
    [SerializeField] private float _width;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.left * _scrollSpeed * Time.deltaTime);

        if (transform.position.x <= -_width)// ”wŒi‚Ì’†SˆÊ’u‚ª”wŒi‚Ì‰¡••ª¶‚És‚Á‚½‚ç
        {
            transform.position += Vector3.right * _width * 2f;//‰E•ûŒü‚É“ñ–‡•ªˆÚ“®
        }
    }
}
