using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject treasure;
    public float delayTime;
    private bool canOpen;
    private bool isOPened;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOPened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(canOpen && !isOPened)
            {
                anim.SetTrigger("Opening");
                isOPened = true;
                Invoke("Gentreasure", delayTime);
            }
        }
    }
    void Gentreasure()
    {
        Instantiate(treasure, transform.position, Quaternion.identity);
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
        }
    }
}
