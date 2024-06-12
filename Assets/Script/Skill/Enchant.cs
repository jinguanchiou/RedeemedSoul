using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchant : MonoBehaviour
{
    public float DestoryTime;
    private Animator PlayerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        PlayerAnim.SetBool("Enchant", true);
    }

    // Update is called once per frame
    void Update()
    {
        DestoryTime -= Time.deltaTime;
        if(DestoryTime <= 0)
        {
            PlayerAnim.SetBool("Enchant", false);
            Destroy(gameObject);
        }
    }
}
