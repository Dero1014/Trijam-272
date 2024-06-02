using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CodeSequence
{
    None,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Master : MonoBehaviour
{
    public int HP = 0;
    public float MaxWaitTime = 1f;

    public CodeSequence[] HowToRespond;

    private CodeSequence _response = CodeSequence.None;
    
    private float _hpTime = 0;

    private Signal _recievedSignal = null;

    public static Master instance;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        PlayerInput();
        if (_recievedSignal != null)
        {
            
            if (Timer())
            {
                HP--;
                print("Ouch I was thinking too much");
            }

            if (_response != CodeSequence.None)
            {
                _recievedSignal.SetCodeSequence(_response);
                _recievedSignal.SendToSlave();
                _recievedSignal = null;
                _response = CodeSequence.None;
                _hpTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger Master");
        if (collision.tag == "Signal")
        {
            print("SIGNAL!");
            _recievedSignal = collision.GetComponentInParent<Signal>();
        }
    }

    bool Timer()
    {
        bool result = false;

        _hpTime += Time.deltaTime;
        if (_hpTime >= MaxWaitTime)
        {
            result = true;
            _hpTime = 0;
        }

        return result;
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            _response = CodeSequence.UP;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            _response = CodeSequence.LEFT;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            _response = CodeSequence.DOWN;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            _response = CodeSequence.RIGHT;

        if (_response != CodeSequence.None && _recievedSignal == null)
        {
            _response = CodeSequence.None;
            HP--;
        }
    }    
}
