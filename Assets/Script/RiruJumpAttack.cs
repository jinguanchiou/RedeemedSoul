using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiruJumpAttack : MonoBehaviour
{
    public int damage;
    private Transform playerTransform;
    private Transform Transform_log;
    private bool Trigger;
    private PolygonCollider2D polygon;
    private PlayerHealth playerHealth;
    private Riru3DController Riru3D;
    private RiruAI riruAI;
    private bool hasTurn;
    // Start is called before the first frame update
    void Start()
    {
        Trigger = true;
        polygon = GetComponent<PolygonCollider2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        riruAI = GameObject.FindGameObjectWithTag("Riru").GetComponent<RiruAI>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Riru3D = GameObject.FindGameObjectWithTag("Riru3D").GetComponent<Riru3DController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            turnAttack();
        }
        if(Trigger && polygon.enabled)
        {
            Trigger = false;
            Transform_log = null;
        }
        else if(!Trigger && !polygon.enabled)
        {
            Trigger = true;
            if (Transform_log == null)
            {
                riruAI.FormJumpAttack(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null && other != null)
            {
                playerHealth.GetComponent<PlayerHealthAI>().DamagePlayer(damage);
                riruAI.FormJumpAttack(true);
                Transform_log = other.transform;
            }
        }
        if (other.gameObject.layer == 18 && other.GetComponent<Shield>())
        {
            other.GetComponent<Shield>().TakeDamage(damage);
            riruAI.FormJumpAttack(true);
        }
    }
    void turnAttack()
    {
        if (!Riru3D.isJumpAttack)
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
