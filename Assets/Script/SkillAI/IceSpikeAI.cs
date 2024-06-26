using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeAI : MonoBehaviour
{
    public Vector2 startSpeed;
    public int Damage;
    public float DestroyTime;
    private Rigidbody2D rb2d;
    private Animator anim;
    private PlayerControllerAI aI;
    // Start is called before the first frame update
    void Start()
    {
        aI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerAI>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb2d.velocity = transform.right * startSpeed.x;
        if (rb2d.velocity.x < 0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (rb2d.velocity.x > -0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CountDestroyTime());
    }

    void DestroyThisIce()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyMonsterGhost>())
            {
                other.GetComponent<EnemyMonsterGhost>().TakeDamage(Damage);
                other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
                other.GetComponent<EnemyMonsterGhost>().Frozen(0.5f, 2);
            }
            if (other.GetComponent<EnemyMontherPig>())
            {
                other.GetComponent<EnemyMontherPig>().TakeDamage(Damage);
                other.GetComponent<EnemyMontherPig>().Frozen(0.5f, 2);
            }
            DestroyThisIce();
        }
        if (other.gameObject.CompareTag("Riru"))
        {
            other.GetComponent<RiruAI>().TakeDamage(Damage);
            other.GetComponent<RiruAI>().Frozen(0.5f, 2);
            aI.AddPoints(Damage);
            DestroyThisIce();
        }
    }
    IEnumerator CountDestroyTime()
    {
        yield return new WaitForSeconds(DestroyTime);
        aI.DeductedPoints(5);
        DestroyThisIce();
    }

}
