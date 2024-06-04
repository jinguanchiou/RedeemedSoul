using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sing : MonoBehaviour
{

    public GameObject dialogBox;
    public Text dialogBoxText;
    public string sigeText;
    private bool isPlayerInSige;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isPlayerInSige)
        {
            dialogBoxText.text = sigeText;
            dialogBox.SetActive(true);
        }
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSige = true;
        }
    }
     void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSige = false;
            dialogBox.SetActive(false);
        }
    }
}
