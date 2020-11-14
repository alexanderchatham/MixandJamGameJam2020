using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorCloser : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator door;


    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("close door");
        door.SetBool("close", true);
    }
}
