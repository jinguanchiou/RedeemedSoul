using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    public GameObject target;
    private PolygonCollider2D polygon;
    private PlayerControllerAI aI;
    private RiruAI riruAI;
    // Start is called before the first frame update
    void Start()
    {
        aI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerAI>();
        riruAI = GameObject.FindGameObjectWithTag("Riru").GetComponent<RiruAI>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Riru"))
        {
            other.transform.position = target.transform.position;
            riruAI.DecPoint(2);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = target.transform.position;
            aI.DeductedPoints(3);
        }
    }
}
