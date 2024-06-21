using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMan : EnemyMontherPig
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            WalkAI();
            AttackAI();
        }
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
                Anim.SetBool("Idle", false);
                Anim.SetBool("Walk", true);
                AttackFlip();
                transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, speed * 2 * Time.deltaTime);
            }
            if(distance < AttackRadius)
            {
                Anim.SetBool("Idle", true);
                Anim.SetBool("Walk", false);
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
                    Anim.SetBool("Walk", true);
                    Anim.SetBool("Idle", false);
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
                        Anim.SetBool("Idle", true);
                        Anim.SetBool("Walk", false);
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
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (direction.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void AttackFlip()
    {
        Vector2 direction = (PlayerTransform.position - transform.position).normalized;
        if (direction.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (direction.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void PlayerHitMe()
    {
        StartCoroutine(GetHit());
    }
    IEnumerator GetHit()
    {
        rb.velocity = new Vector2(transform.right.x * -20, 0f);
        Hit = true;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0f, 0f);
        Hit = false;
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
    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}
