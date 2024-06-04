using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMontherPig : MonoBehaviour
{
    public int health;
    public float flashTime;
    public bool Hit;
    public GameObject bloodEffect;
    public GameObject dropCoin;
    public GameObject floatPoint;

    private SpriteRenderer sr;
    private Color originalColor;

    public float speed;
    public float startWaitTime;
    public float radius;
    public float AttackRadius;
    private float waitTime;


    public EnemyAttack Attack;
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    private Animator Anim;
    private Rigidbody2D rb;
    private Transform PlayerTransform;
    private CircleCollider2D EnemyAttackRadius;

    // Start is called before the first frame update
    public void Start()
    {
        EnemyAttackRadius = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalColor = sr.color;
        Anim = GetComponent<Animator>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }
    // Update is called once per frame
    public void Update()
    {
        if (health > 0 && !Hit)
        {
            Flip();
            WalkAI();
            AttackAI();
        }
        Dead();
    }
    void AttackAI()
    {
        if (PlayerTransform != null)
        {
            float distance = (transform.position - PlayerTransform.position).sqrMagnitude;
            if (distance < radius && distance >= AttackRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, speed * 2 * Time.deltaTime);
            }
        }
    }
    void WalkAI()
    {
        if (PlayerTransform != null)
        {
            float distance = (transform.position - PlayerTransform.position).sqrMagnitude;
            if (distance > radius)
            {

                if (Vector2.Distance(transform.position, movePos.position) >= 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
                }
                if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
                {
                    if (waitTime <= 0)
                    {
                        movePos.position = GetRandomPos();
                        waitTime = startWaitTime;
                        Walk(true);
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                        Walk(false);
                    }
                }
            }
        }
    }
    void Flip()
    {
        if (PlayerTransform != null)
        {
            float distance = (transform.position.x - PlayerTransform.position.x);
            if (PlayerTransform != null)
            {
                if (distance > 0.1f)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                if (distance < -0.1f)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }
    void Dead()
    {
        if (health <= 0)
        {
            Anim.SetBool("Dead", true);
            StartCoroutine(WaitDead());
        }
    }
    IEnumerator WaitDead()
    {
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 2;
        yield return new WaitForSeconds(0.5f);
        Instantiate(dropCoin, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        health -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameController.camShake.Shake();
    }
    public void PlayerHitMe()
    {
        StartCoroutine(GetHit());
    }
    IEnumerator GetHit()
    {
        rb.velocity = new Vector2(transform.right.x * 20, 0f);
        Hit = true;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0f, 0f);
        Hit = false;
    }
    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        sr.color = originalColor;
    }
    void Walk(bool Walk)
    {
        Anim.SetBool("Walk", Walk);
    }
    void Idle(bool Idle)
    {
        Anim.SetBool("Idle", Idle);
    }
    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}
