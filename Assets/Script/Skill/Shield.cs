using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health;
    public float Duration;

    private Animator anim;
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
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth.DestroyShield();
        Protect();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine(ShieldHealthGone());
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
        playerHealth.DestroyShield();
        anim.SetTrigger("Disappear");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    IEnumerator ShieldHealthGone()
    {
        if (playerHealth.ResistDamage <= 0)
        {
            anim.SetTrigger("Disappear");
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
    void Protect()
    {
        playerHealth.Shield(health);
    }
}
