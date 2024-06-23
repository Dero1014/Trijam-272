using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Border other;
    public bool Target;

    public void SetTarget()
    {
        Target = true;
        other.Target = false;

        GetComponent<SpriteRenderer>().color = Color.green;
        other.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void SetOtherTarget()
    {
        other.Target = true;
        Target = false;

        GetComponent<SpriteRenderer>().color = Color.red;
        other.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public bool IsTarget()
    {
        return Target;
    }

}
