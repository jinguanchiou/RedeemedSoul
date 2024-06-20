using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousFlame : MonoBehaviour
{
    public int Damage;
    private bool isAttacking = false;
    private PolygonCollider2D poisonousFlameRange;
    // Start is called before the first frame update
    void Start()
    {
        poisonousFlameRange = GetComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(ResetPoisonousFlameRange());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyMonsterGhost>())
            {
                other.GetComponent<EnemyMonsterGhost>().Toxin();
            }
            if (other.GetComponent<EnemyMontherPig>())
            {
                other.GetComponent<EnemyMontherPig>().Toxin();
            }
        }
        if(other.gameObject.CompareTag("Riru"))
        {
            other.GetComponent<RiruAI>().Toxin();
        }
    }
    IEnumerator ResetPoisonousFlameRange()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}