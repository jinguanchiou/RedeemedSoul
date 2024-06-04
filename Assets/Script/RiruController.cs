using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiruController : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;
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
        turn();
        Walk();
    }
    void Walk()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        riru3DContuoller.Walk(true);
    }
    void turn()
    {
        riru3DContuoller.turn3D(playerTransform, transform);
    }
}
