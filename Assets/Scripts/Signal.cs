using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Signal : MonoBehaviour
{
    public CodeSequence CodeSequence;
    public float Speed;
    public float StopAt;
    public Transform SignalObject;
    public Transform MasterConnection;
    public Transform SlaveConnection;
    public GameObject[] Arrows;

    public float BreakOffset;

    private Rigidbody2D _rb;

    public LineRenderer Pathway;

    private int pathIndex = 3;
    private bool toMaster = false;
    private bool toSlave = false;

    private void Start()
    {
        Pathway = GetComponentInChildren<LineRenderer>();
        _rb = GetComponentInChildren<Rigidbody2D>();
        SetPosition();
        SignalObject.transform.position = Pathway.GetPosition(pathIndex);

    }

    private void Update()
    {
        if (toMaster)
        {
            GoToMaster();
        }else if(toSlave)
        {
            GoToSlave();
        }
    }

    public void SendToMaster()
    {
        toMaster = true;
        toSlave = false;
    }

    public void SendToSlave()
    {
        toMaster = false;
        toSlave = true;
    }

    void GoToMaster()
    {
        if (pathIndex == 4)
        {
            pathIndex = 3;
        }
        if (pathIndex >= 0)
        {
            var direction = Pathway.GetPosition(pathIndex) - SignalObject.position;
            if (direction.magnitude >= StopAt)
                _rb.velocity = direction.normalized * Speed;
            else if (direction.magnitude < StopAt)
            {
                SignalObject.position = Pathway.GetPosition(pathIndex);
                pathIndex--;
                _rb.velocity = Vector2.zero;
            }
        }
    }

    void GoToSlave()
    {
        if (pathIndex == -1)
        {
            pathIndex = 0;
        }
        if (pathIndex <= 3)
        {
            var direction = Pathway.GetPosition(pathIndex) - SignalObject.position;
            if (direction.magnitude >= StopAt)
                _rb.velocity = direction.normalized * Speed;
            else if (direction.magnitude < StopAt)
            {
                print("STOP!");
                SignalObject.position = Pathway.GetPosition(pathIndex);
                pathIndex++;
                _rb.velocity = Vector2.zero;
            }
        }

    }

    void SetPosition()
    {
        if (Pathway != null)
        {
            Pathway.SetPosition(0, MasterConnection.position);

            // half way point
            var SecondPoint = new Vector2(MasterConnection.position.x + BreakOffset, MasterConnection.position.y);
            Pathway.SetPosition(1, SecondPoint);

            // Up
            var ThirdPoint = new Vector2(SecondPoint.x, SlaveConnection.position.y);
            Pathway.SetPosition(2, ThirdPoint);

            Pathway.SetPosition(3, SlaveConnection.position);
        }
    }

    public void SetCodeSequence(CodeSequence newSequence)
    {
        CodeSequence = newSequence;
        switch (newSequence) 
        {
            case CodeSequence.None:
                Arrows[0].SetActive(false);
                Arrows[1].SetActive(false);
                Arrows[2].SetActive(false);
                Arrows[3].SetActive(false);
                break;
            case CodeSequence.UP:
                Arrows[0].SetActive(true);
                Arrows[1].SetActive(false);
                Arrows[2].SetActive(false);
                Arrows[3].SetActive(false);
                break;
            case CodeSequence.DOWN:
                Arrows[0].SetActive(false);
                Arrows[1].SetActive(true);
                Arrows[2].SetActive(false);
                Arrows[3].SetActive(false);
                break;
            case CodeSequence.LEFT:
                Arrows[0].SetActive(false);
                Arrows[1].SetActive(false);
                Arrows[2].SetActive(true);
                Arrows[3].SetActive(false);
                break;
            case CodeSequence.RIGHT:
                Arrows[0].SetActive(false);
                Arrows[1].SetActive(false);
                Arrows[2].SetActive(false);
                Arrows[3].SetActive(true);
                break;

            default:
                break;
        }
    }
}
