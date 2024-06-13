using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public int TreasureBoxID;
    public TreasureContorller treasureContorller;
    public GameObject treasureBox;
    public GameObject treasure;
    public int treasureQuantity;
    public float delayTime;
    private SpriteRenderer renderer;
    private float minLocation = -0.5f;
    private float maxLocation = 0.5f;
    private bool canOpen;
    private bool isOPened;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOPened = false;
        renderer = treasureBox.GetComponent<SpriteRenderer>();
        renderer.enabled = false;
        if(treasureContorller.TreasureList[TreasureBoxID])
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
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
        StartCoroutine(WaitTime());
        treasureContorller.TreasureList[TreasureBoxID] = true;
    }
    IEnumerator WaitTime()
    {
        for (int i = 0; i < treasureQuantity; i++)
        {
            yield return new WaitForSeconds(0.1f);
            float randomLocation = Random.Range(minLocation, maxLocation);
            Instantiate(treasure, new Vector3(transform.position.x + randomLocation, transform.position.y + 0.5f + randomLocation), Quaternion.identity);
        }
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D" && renderer.enabled)
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
    public void Appear()
    {
        renderer.enabled = true;
    }
}
