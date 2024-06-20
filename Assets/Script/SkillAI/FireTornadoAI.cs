using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornadoAI : MonoBehaviour
{
    public Vector2 startSpeed;
    public int Damage;
    public float InLoopTime;
    public float becomeSmallerTime;
    public float DestroyTime;
    public int Frequency;
    public int BurningDamage;

    private bool IsStart;
    private Rigidbody2D rb2d;
    private Animator anim;
    private PolygonCollider2D Trigger;
    private PlayerControllerAI aI;
    private GameObject RiruHitBox;
    // Start is called before the first frame update
    void Start()
    {
        aI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerAI>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Trigger = GetComponent<PolygonCollider2D>();

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
        StartCoroutine(CountInLoopTime());
        for (int i = 0; i < (int)(InLoopTime + becomeSmallerTime) / 2; i++)
        {
            if (!IsStart)
            {
                IsStart = true;
                StartCoroutine(DemageInterval());
            }
        }
    }

    void DestroyThisSkill()
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
                other.GetComponent<EnemyMonsterGhost>().Burning(BurningDamage, Frequency);
            }
            if (other.GetComponent<EnemyMontherPig>())
            {
                other.GetComponent<EnemyMontherPig>().TakeDamage(Damage);
                other.GetComponent<EnemyMontherPig>().Burning(3, 3);
            }
        }
        if (other.gameObject.CompareTag("Riru"))
        {
            other.GetComponent<RiruAI>().TakeDamage(Damage);
            other.GetComponent<RiruAI>().Burning(3, 3);
            RiruHitBox = other.gameObject;
            aI.AddPoints(Damage);
        }
    }
    IEnumerator CountInLoopTime()
    {
        yield return new WaitForSeconds(InLoopTime);
        anim.SetTrigger("InLoop");
        yield return new WaitForSeconds(becomeSmallerTime);
        anim.SetTrigger("InDestroy");
        yield return new WaitForSeconds(DestroyTime);
        if(RiruHitBox == null)
        {
            aI.DeductedPoints(10);
        }
        DestroyThisSkill();
    }
    IEnumerator DemageInterval()
    {
        Trigger.enabled = true;
        yield return new WaitForSeconds(0.25f);
        Trigger.enabled = false;
        yield return new WaitForSeconds(0.25f);
        IsStart = false;
    }

}
