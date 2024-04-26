using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiruController : MonoBehaviour
{
    PlayerController playerController;
    Riru3DController riru3DContuoller;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        riru3DContuoller = GameObject.FindGameObjectWithTag("Riru").GetComponent<Riru3DController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
