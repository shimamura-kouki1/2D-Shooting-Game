using System;
using UnityEngine;

public class Hit3 : MonoBehaviour
{
    [SerializeField] private Transform[] _enemy;
    [SerializeField] private float hitRadius = 1f;
    void Start()
    {

    }

    void Update()
    {

    }
    public bool IsCircleColliding(float x1, float y1, float r1, float x2, float y2, float r2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;
        float distance = MathF.Sqrt(dx * dx + dy * dy);
        return distance < (r1 + r2);
    }
}
