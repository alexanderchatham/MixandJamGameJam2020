using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : interactable
{
    public GameObject other;
    public bool triggerOther = false;
    public bool triggerOn = false;
    public enum collectableType
    {
        seed,
        gold
    }
    public collectableType type = collectableType.seed;
    public override void trigger()
    {
        if(type == collectableType.seed)
            GameObject.FindGameObjectWithTag("Player").GetComponent<character>().getSeed(1);
        else
            GameObject.FindGameObjectWithTag("Player").GetComponent<character>().getGold(1);

        if (triggerOther)
        {
            if (triggerOn)
            {
                other.SetActive(true);
            }
            else
                other.SetActive(false);
        }
        Destroy(gameObject, .1f);
        GetComponent<Collider>().enabled = false;

    }
}
