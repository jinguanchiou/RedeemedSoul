using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    private CircleCollider2D EnemyCanAttackRange;

    public bool isAttacking = false;
    public EnemyAttack Attack;
    public FireAttackRange SpecialAttack;

    // Start is called before the first frame update
    void Start()
    {
        EnemyCanAttackRange = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Debug.Log("A");
                if (!isAttacking)
                {
                    isAttacking = true;
                    EnemyCanAttackRange.enabled = false;
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
        EnemyCanAttackRange.enabled = true;
    }
    
}
