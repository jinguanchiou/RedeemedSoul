using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health;
    public float Duration;

    private Animator anim;
    private CircleCollider2D ShieldRange;
    private Transform PlayerTransform;
    private Rigidbody2D rb2d;
    private PlayerHealth playerHealth;
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        ShieldRange = GetComponent<CircleCollider2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Protect(true);
        Duration -= Time.deltaTime;
        DurationCount();
        if (PlayerTransform != null)
        {
            transform.position = PlayerTransform.position;
            transform.rotation = PlayerTransform.rotation;
        }
    }
    void DurationCount()
    {
        Duration -= Time.deltaTime;
        if (Duration <= 3)
        {
            StartCoroutine(DestroyShield());
        }
    }
    IEnumerator DestroyShield()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            sp.enabled = false;
            yield return new WaitForSeconds(0.5f);
            sp.enabled = true;
        }
        anim.SetTrigger("Disappear");
        yield return new WaitForSeconds(0.5f);
        Protect(false);
        Destroy(gameObject);
    }
    void TakeDamage(int Dameage)
    {
        health -= Dameage;
        if(health <= 0)
        {
            StartCoroutine(ShieldHealthGone());
            Protect(false);
        }
    }
    IEnumerator ShieldHealthGone()
    {
        anim.SetTrigger("Disappear");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    void Protect(bool hasShield)
    {
        playerHealth.Shield(hasShield);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
            Debug.Log("e04");
            TakeDamage(1);
        }
    }
}
