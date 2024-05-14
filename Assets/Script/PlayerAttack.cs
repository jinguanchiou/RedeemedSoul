using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float startTime;
    public float time;

    private bool isAttacking = false;
    private bool AttackisFinish = false;
    private bool isAttacking_2 = false;
    private Animator anim;
    private PolygonCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        if (Input.GetButtonDown("Attack") && !AttackisFinish && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            Debug.Log("AttackIng");
            StartCoroutine(StartAttack());
        }
        if (Input.GetButtonDown("Attack") && AttackisFinish && !isAttacking_2)
        {
            isAttacking_2 = true;
            anim.SetTrigger("Attack_2");
            Debug.Log("Attack_2Ing");
            StartCoroutine(StartAttack_2());
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
        isAttacking = false;
        AttackisFinish = true;
    }

    IEnumerator StartAttack_2()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
        isAttacking_2 = false;
        AttackisFinish = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            other.GetComponent<EnemyMonsterGhost>().TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("Riru"))
        {
            other.GetComponent<RiruAI>().TakeDamage(damage);
        }
    }
}
