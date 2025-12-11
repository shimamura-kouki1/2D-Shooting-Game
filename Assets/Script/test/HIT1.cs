using System.Collections.Generic;
using UnityEngine;

public class HIT1 : MonoBehaviour
{
    [SerializeField] private List<Transform> enemies;
    [SerializeField] private float longRadius = 1f;
    [SerializeField] private float shortRadius = 0.5f;

    void Update()
    {
        Vector2 c = transform.position;

        foreach (var e in enemies)
        {
            Vector2 p = e.position;

            float dx = p.x - c.x;
            float dy = p.y - c.y;

            float val =
                (dx * dx) / (longRadius * longRadius) +
                (dy * dy) / (shortRadius * shortRadius);

            if (val <= 1f)
            {
                Debug.Log("aaa");
                Destroy(this.gameObject);
                //Destroy(gameObject);
                break;
            }
        }
    }
}
