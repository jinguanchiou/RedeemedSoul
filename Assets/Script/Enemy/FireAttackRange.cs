using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackRange : MonoBehaviour
{
    private CircleCollider2D EnemyFireCanAttackRange;
    public bool isAttacking = false;

    public BreathingFire Attack;

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
                    StartCoroutine(ResetAttackFlag());
                }
        }
    }
    IEnumerator ResetAttackFlag()
    {
        yield return new WaitForSeconds(1f);
        Attack.Attack();
        yield return new WaitForSeconds(2f);
        isAttacking = false;
        EnemyFireCanAttackRange.enabled = true;
    }
}
