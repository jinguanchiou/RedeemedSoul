using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiruThrustAttack : MonoBehaviour
{
    public int damage;
    private Transform playerTransform;
    [SerializeField] private PolygonCollider2D polygon;
    private bool Trigger;
    private Transform Playertransform;
    private PlayerHealthAI playerHealth;
    private RiruAI riruAI;
    private Riru3DController Riru3D;
    private bool hasTurn;
    // Start is called before the first frame update
    void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthAI>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        riruAI = GameObject.FindGameObjectWithTag("Riru").GetComponent<RiruAI>();
        Riru3D = GameObject.FindGameObjectWithTag("Riru3D").GetComponent<Riru3DController>();
        Trigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Trigger && polygon.enabled)
        {
            Trigger = false;
            Playertransform = null;
        }
        else if(!Trigger && !polygon.enabled)
        {
            Trigger = true;
            if(Playertransform == null)
            { riruAI.FormThrustAttack(false); }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                Debug.Log("HitBox");
                playerHealth.GetComponent<PlayerHealth>().DamagePlayer(damage);
                riruAI.FormThrustAttack(true);
                Playertransform = other.transform;
            }
        }
        if (other.gameObject.layer == 18 && other.GetComponent<Shield>())
        {
            other.GetComponent<Shield>().TakeDamage(damage);
            riruAI.FormThrustAttack(true);
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
