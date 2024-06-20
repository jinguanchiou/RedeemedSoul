using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiruBlock : MonoBehaviour
{
    private Transform playerTransform;
    private Riru3DController Riru3D;
    private RiruAI riruAI;
    private bool hasTurn;
    private PolygonCollider2D polygon;
    private int Count;
    // Start is called before the first frame update
    void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        riruAI = GameObject.FindGameObjectWithTag("Riru").GetComponent<RiruAI>();
        Riru3D = GameObject.FindGameObjectWithTag("Riru3D").GetComponent<Riru3DController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            turnAttack();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TriggeredSkill") || other.gameObject.CompareTag("BoolSkill"))
        {
            Riru3D.Block();
            riruAI.FormBlock(true);
            if (Count > 2)
            {
                polygon.enabled = false;
                riruAI.BlockCoolDownTime = riruAI.BlockCoolDownTime_log;
                Count = 0;
            }
            Destroy(other.gameObject);
            Count++;
            if (hasTurn)
                riruAI.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            else if (!hasTurn)
                riruAI.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
        }
    }
    void turnAttack()
    {
        if (!Riru3D.isBlocking)
        {
            if (playerTransform.position.x < transform.position.x && !hasTurn && playerTransform != null)
            {
                Quaternion turnRotation = Quaternion.Euler(0, 180, 0);
                transform.rotation = turnRotation;
                hasTurn = true;
            }
            else if (playerTransform.position.x > transform.position.x && hasTurn && playerTransform != null)
            {
                Quaternion turnRotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = turnRotation;
                hasTurn = false;
            }
        }
    }
}
