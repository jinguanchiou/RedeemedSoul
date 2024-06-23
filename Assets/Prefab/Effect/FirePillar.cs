using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    public float StartTime;
    public float EndTime;
    public float WaitDestroy;
    public int damage;
    private bool Alreadyadd;
    private RiruAI riruAI;
    private Animator Anim;
    private BoxCollider2D HitBox;
    private PlayerHealth playerHealth;
    private GameObject PlayerHitBox;
    void Start()
    {
        Anim = GetComponent<Animator>();
        riruAI = GameObject.FindGameObjectWithTag("Riru").GetComponent<RiruAI>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        HitBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartTime -= Time.deltaTime;
        if(StartTime <= 0)
        {
            OpenHitBox();
        }
        if(StartTime <= 0.15f)
        {
            Anim.SetTrigger("Start");
        }
    }
    void OpenHitBox()
    {
        if (!Alreadyadd)
        {
            Alreadyadd = true;
            transform.position = new Vector2(transform.position.x, transform.position.y + 3.52f);
            HitBox.enabled = true;
        }
        EndTime -= Time.deltaTime;
        if(EndTime <= 0)
        {
            ColseHitBox();
        }
    }
    void ColseHitBox()
    {
        WaitDestroy -= Time.deltaTime;
        HitBox.enabled = false;
        if (WaitDestroy <= 0)
        {
            if (PlayerHitBox == null)
            { 
                riruAI.FormFirePillar(false);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            playerHealth.GetComponent<PlayerHealth>().DamagePlayer(damage);
            playerHealth.GetComponent<PlayerHealth>().Burning(2, 2);
            riruAI.FormFirePillar(true);
            PlayerHitBox = other.gameObject;
        }
        if (other.gameObject.layer == 18 && other.GetComponent<Shield>())
        {
            other.GetComponent<Shield>().TakeDamage(damage);
            riruAI.FormFirePillar(true);
        }
    }
}
