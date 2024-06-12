using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnchant : MonoBehaviour
{
    public float DestoryTime;
    private Animator PlayerAnim;

    // Start is called before the first frame update
    void Start()
    {
        PlayerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        PlayerAnim.SetBool("FireEnchant", true);
    }

    // Update is called once per frame
    void Update()
    {
        DestoryTime -= Time.deltaTime;
        if (DestoryTime <= 0)
        {
            PlayerAnim.SetBool("FireEnchant", false);
            Destroy(gameObject);
        }
    }
}
