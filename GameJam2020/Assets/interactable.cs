using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    public bool collectable = false;
    public virtual void trigger()
    {
        Debug.Log("triggered");
    }

    public virtual void activate(character player)
    {

        Debug.Log("activated");
    }

    public virtual void leave()
    {
        Debug.Log("left trigger");
    }
}
