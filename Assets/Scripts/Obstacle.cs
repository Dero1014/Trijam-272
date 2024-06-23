using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float _obstacleFallSpeed = 1;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
        _rb.velocity = Vector2.down * Time.fixedDeltaTime * _obstacleFallSpeed;
        DestroyCondition();
    }

    public void SetObstacleSpeed(float speed)
    {
        _obstacleFallSpeed = speed;
    }

    void DestroyCondition()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
