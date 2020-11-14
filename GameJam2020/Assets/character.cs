using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class character : MonoBehaviour
{
    public TextMeshProUGUI seedText;
    public TextMeshProUGUI goldText;
    Rigidbody rb;
    public float jumpSpeed = 5f;
    public float moveSpeed = 5f;
    public PhysicMaterial charMat;
    [SerializeField]
    bool grounded = true;
    [SerializeField]
    bool falling = false;
    interactable selected;
    Vector3 startingPos;
    public int gold = 0;
    public int seedCount = 0;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPos = transform.position;
        seedText.text = "Seeds: " + seedCount;
        goldText.text = "Gold: " + gold;
    }
    public void useSeed()
    {
        seedCount--;
        seedText.text = "Seeds: " + seedCount;
    }
    public void getSeed(int i)
    {
        seedCount+= i;
        seedText.text = "Seeds: " + seedCount;
    }
    public void spendSeed(int i)
    {
        seedCount -= i;
        seedText.text = "Seeds: " + seedCount;
    }
    public void getGold(int i)
    {
        gold+= i;
        goldText.text = "Gold: " + gold;
    }
    public void spendGold(int i)
    {
        gold -= i;
        goldText.text = "Gold: " + gold;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded && !falling)
        {
            grounded = false;
            Debug.Log("Jump");
            rb.velocity = new Vector3(rb.velocity.x, 0);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            Physics.gravity = Vector3.down * 10;
        }

        if (Input.GetButtonUp("Jump") && !falling)
        {
            falling = true;
            Physics.gravity = Vector3.down * 20;
        }

        
        if (Input.GetButton("Horizontal"))
        {
            charMat.dynamicFriction = 0f;
            charMat.staticFriction = 0f;
            if(Input.GetAxis("Horizontal") * rb.velocity.x < 0)
            {

                charMat.dynamicFriction = .8f;
                charMat.staticFriction = .8f;
            }
            float mult = 1;
            if (!grounded)
                mult = mult * 2;
            if(rb.velocity.x > 5f || rb.velocity.x < -5f)
            {

            }
            else
                rb.AddForce( new Vector3(Input.GetAxis("Horizontal") *moveSpeed * mult,0), ForceMode.Acceleration);

        }
        else
        {

            charMat.dynamicFriction = .5f;
            charMat.staticFriction = .5f;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            if(selected)
                selected.activate(this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            grounded = true;
            falling = false;
        }
        if(collision.gameObject.tag == "Lava")
        {
            transform.position = startingPos;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            falling = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<interactable>())
        {
            selected = other.gameObject.GetComponent<interactable>();
            if (selected.collectable)
                selected = null;
            other.gameObject.GetComponent<interactable>().trigger();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<interactable>())
        {
            selected = null;
            other.gameObject.GetComponent<interactable>().leave();
        }
    }
}
