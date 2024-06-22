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

    protected SpriteRenderer sr;
    protected Color originalColor;

    public float speed;
    public float startWaitTime;
    public float radius;
    public float AttackRadius;
    protected float waitTime;
    protected int toxin;
    protected float toxinTime;
    protected float speed_Log;
    protected float minLocation = -0.5f;
    protected float maxLocation = 0.5f;

    protected Coroutine burningCoroutine;
    protected Coroutine frozenCoroutine;
    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    protected Transform target;
    protected Animator Anim;
    protected Rigidbody2D rb;
    protected Transform PlayerTransform;
    protected CircleCollider2D EnemyAttackRadius;

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
        speed_Log = speed;
    }
    // Update is called once per frame
    public void Update()
    {
        if (health > 0 && !Hit)
        {
            WalkAI();
            AttackAI();
        }
        Idle();
        Walk();
        Dead();
        ToxinTime();
    }
    private void AttackAI()
    {
        if (PlayerTransform != null)
        {
            float distance = (transform.position - PlayerTransform.position).sqrMagnitude;
            if (distance < radius && distance >= AttackRadius)
            {
                AttackFlip();
                transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, speed * 2 * Time.deltaTime);
            }
        }
    }
    private void WalkAI()
    {
        if (PlayerTransform != null)
        {
            
            float distance = (transform.position - PlayerTransform.position).sqrMagnitude;
            if (distance > radius)
            {
                WalkFlip();
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
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }
            }
        }
    }
    private void WalkFlip()
    {
        Vector2 direction = (movePos.position - transform.position).normalized;
        if (direction.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (direction.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void AttackFlip()
    {
        Vector2 direction = (PlayerTransform.position - transform.position).normalized;
        if (direction.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (direction.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Dead()
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
    public void Burning(int damage, int frequency)
    {
        if (burningCoroutine != null)
        {
            StopCoroutine(burningCoroutine);
        }
        burningCoroutine = StartCoroutine(BurningIE(damage, frequency));
    }
    IEnumerator BurningIE(int damage, int frequency)
    {
        for (int i = 0; i < frequency; i++)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }
        burningCoroutine = null;
    }
    public void Frozen(float DecreasePercentage, float duration)
    {
        if (frozenCoroutine != null)
        {
            StopCoroutine(frozenCoroutine);
        }
        frozenCoroutine = StartCoroutine(FrozenIE(DecreasePercentage, duration));
    }
    IEnumerator FrozenIE(float DecreasePercentage, float duration)
    {
        if (speed_Log == speed)
        {
            speed *= DecreasePercentage;
        }
        yield return new WaitForSeconds(duration);
        speed = speed_Log;
        frozenCoroutine = null;
    }
    public void Toxin()
    {
        if (toxin < 8)
        {
            toxin++;
        }
        toxinTime = 3;
        TakeDamage(toxin);
    }
    public void ToxinTime()
    {
        if (toxinTime > 0)
        {
            toxinTime -= Time.deltaTime;
        }
        if (toxinTime <= 0)
        {
            toxin = 0;
        }
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
    public void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    public void ResetColor()
    {
        sr.color = originalColor;
    }
    private void Walk()
    {
        Vector2 direction = (movePos.position - transform.position);
        if (direction.x <= 0.1f && direction.x >= -0.1f)
        {
            Anim.SetBool("Walk", false);
            
        }
        else
            Anim.SetBool("Walk", true);

    }
    private void Idle()
    {
        Vector2 direction = (movePos.position - transform.position);
        if (direction.x <= 0.1f && direction.x >= -0.1f)
        {
            Anim.SetBool("Idle", true);
        }
        else
            Anim.SetBool("Idle", false);
    }
    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}
