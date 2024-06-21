using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAttackRange : MonoBehaviour
{
    private CircleCollider2D EnemyFireCanAttackRange;
    public Animator anim;
    public GameObject Poison;
    public float StartTime;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        EnemyFireCanAttackRange = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (!isAttacking)
            {
                isAttacking = true;
                EnemyFireCanAttackRange.enabled = false;
                StartCoroutine(HitBoxOpenTime());
                anim.SetTrigger("Attack");
            }
        }
    }
    IEnumerator HitBoxOpenTime()
    {
        yield return new WaitForSeconds(StartTime);
        Instantiate(Poison, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        yield return new WaitForSeconds(3);
        isAttacking = false;
        EnemyFireCanAttackRange.enabled = true;
    }
}
