using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gardenBed : interactable
{
    public GameObject ui;

    public GameObject[] produce;
    public int produceIndex = 0;
    public int produceStage = 0;
    bool selected;

    public enum plantState
    {
        unplanted,
        growing,
        ready
    }
    public plantState state = plantState.unplanted;
    public override void trigger()
    {
        selected = true;
        ui.SetActive(true);
    }
    public override void activate(character player)
    {
        if(player.seedCount > 0 && state == plantState.unplanted)
        {
            player.useSeed();
            int random = Random.Range(0, produce.Length);
            produce[random].transform.GetChild(0).gameObject.SetActive(true);
            produceIndex = random;
            produceStage = 0;
            state = plantState.growing;
            growthRoutine = StartCoroutine(growPlant());
        }

        if (state == plantState.ready)
        {
            Debug.Log("Collected Plant");
            state = plantState.unplanted;
            produce[produceIndex].transform.GetChild(produceStage).gameObject.SetActive(false);
            plantReady.Stop();
        }
        base.activate(player);
    }
    public override void leave()
    {
        selected = false;
        ui.SetActive(false);
    }
    Coroutine growthRoutine;
    public float growthInterval = 25f;
    public ParticleSystem plantReady;
    IEnumerator growPlant()
    {
        float timer = 0f;
        while (timer < growthInterval)
        {

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (produceStage == produce[produceIndex].transform.childCount -1 )
        {
            state = plantState.ready;
            plantReady.Play();
        }
        else
        {

            produce[produceIndex].transform.GetChild(produceStage).gameObject.SetActive(false);
            produceStage++;
            produce[produceIndex].transform.GetChild(produceStage).gameObject.SetActive(true);
            growthRoutine = StartCoroutine(growPlant());
        }
    }
}
