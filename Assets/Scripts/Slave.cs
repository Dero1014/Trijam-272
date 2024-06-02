using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slave : MonoBehaviour
{
    public List<CodeSequence> codeSequences = new List<CodeSequence>();
    public CodeSequence expectedResponse;
    public SpriteRenderer spriteRenderer;
    public int WaitForSeconds;

    public bool Sent;

    public int HP;


    void Start()
    {
        Sent = false;
    }
    int SequenceIndex(CodeSequence sequence)
    {
        int index = -1;

        switch (sequence)
        {
            case CodeSequence.UP:
                index = 0; break;
            case CodeSequence.DOWN:
                index = 1; break;
            case CodeSequence.LEFT:
                index = 2; break;
            case CodeSequence.RIGHT:
                index = 3; break;
            default:
                break;
        }

        return index;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        print("Trigger Slave");
        if (collider.gameObject.tag == "Signal")
        {
            if (Sent == false)
            {
                Sent = true;
                IEnumerator coroutine = StartIn(collider);
                StartCoroutine(coroutine);
            }
            else
            {
                ReadSignal(collider.gameObject.GetComponentInParent<Signal>());
            }

        }
    }

    IEnumerator StartIn(Collider2D collider)
    {
        print("Waiting");
        yield return new WaitForSeconds(WaitForSeconds);
        print("Done");
        SendSequence(collider.gameObject.GetComponentInParent<Signal>());
        collider.gameObject.GetComponentInParent<Signal>().SendToMaster();
    }

    void ReadSignal(Signal signal)
    {
        if (signal.CodeSequence == expectedResponse)
        {
            codeSequences.RemoveAt(0);

            if (codeSequences.Count > 0)
                SendSequence(signal);
            else
            {
                signal.SignalObject.gameObject.SetActive(false);
                signal.Pathway.SetColors(Color.gray, Color.gray);
                spriteRenderer.color = Color.green;
            }
        }
        else
        {
            SendSequence(signal);
            HP--;
        }
    }

    public void SendSequence(Signal signal)
    {
        signal.SetCodeSequence(codeSequences[0]);
        expectedResponse = Master.instance.HowToRespond[SequenceIndex(codeSequences[0])];
        signal.SendToMaster();
    }
}
