using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItem : interactable
{
    public GameObject ui;
    public enum cost
    {
        seeds,
        gold
    }
    public GameObject itemToActivate;
    public ParticleSystem itemCelebration;
    public cost thisCost;
    public int amount;
    public override void trigger()
    {
        ui.SetActive(true);
    }

    public override void leave()
    {
        ui.SetActive(false);
    }
    public override void activate(character player)
    { 
        if(thisCost == cost.seeds)
        {
            if(player.seedCount >= amount)
            {
                Debug.Log("buy with seeds");
                player.spendSeed(amount);
                itemToActivate.SetActive(true);

                itemCelebration.transform.parent = null;
                itemCelebration.Play();
                gameObject.SetActive(false);

            }
        }
        if(thisCost == cost.gold)
        {
            if(player.gold >= amount)
            {
                Debug.Log("buy with gold");
                player.spendGold(amount);
                itemToActivate.SetActive(true);
                itemCelebration.transform.parent = null;
                itemCelebration.Play();
                gameObject.SetActive(false);

            }
        }
    }

}
