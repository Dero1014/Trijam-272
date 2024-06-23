using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed= 1;

    private Rigidbody2D _rb;

    private bool _move = false;
    private int _direction = 1;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.PlayAudioSource(2);

            if (_move == false)
            {
                GameManager.instance.ClearGame(this);
                FindObjectOfType<Spawner>().SetSpawner(true);
                FindObjectOfType<UIController>().ShowScreen(1);
                _move = true;
            }
            ChangeDirection();
        }

    }

    public void DontMove()
    {
        _rb.velocity = Vector3.zero;
        _move = false;
    }

    private void FixedUpdate()
    {
        if (_move)
        {
            _rb.velocity = Vector2.left * Time.fixedDeltaTime * _direction * Speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            if (collision.GetComponent<Border>().IsTarget())
            {
                AudioManager.instance.PlayAudioSource(0);
                GameManager.instance.Score(10);
                collision.GetComponent<Border>().SetOtherTarget();
            }
            else
            {
                AudioManager.instance.PlayAudioSource(1);
                GameManager.instance.Score(-10);
            } 
        }
        else
        {
            // Player dies
            GameManager.instance.StopGame(this);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BL")
        {
            ChangeDirection(-1);
        }
        if (collision.gameObject.name == "BR")
        {
            ChangeDirection(1);
        }
    }

    void ChangeDirection()
    {
        _direction *= -1;
    }

    void ChangeDirection(int dir)
    {
        _direction = dir;
    }

}
