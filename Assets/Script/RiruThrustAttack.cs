using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiruThrustAttack : MonoBehaviour
{
    public int damage;
    public Transform playerTransform;
    private PlayerHealth playerHealth;
    private RiruAI riruAI;
    private bool hasTurn;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        riruAI = GameObject.FindGameObjectWithTag("Riru").GetComponent<RiruAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.GetComponent<PlayerHealth>().DamagePlayer(damage);
                riruAI.GetComponent<RiruAI>().FormThrustAttack(true);
            }
        }
    }
    public void turnAttack()
    {
        if (playerTransform.position.x < transform.position.x && !hasTurn)
        {
            Quaternion turnRotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = turnRotation;
            hasTurn = true;
        }
        else if (playerTransform.position.x > transform.position.x && hasTurn)
        {
            Quaternion turnRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = turnRotation;
            hasTurn = false;
        }
    }
}