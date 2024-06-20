using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousFlameAI : MonoBehaviour
{
    public int Damage;
    private bool isAttacking = false;
    private PolygonCollider2D poisonousFlameRange;
    private PlayerControllerAI aI;
    private GameObject RiruHitBox;
    // Start is called before the first frame update
    void Start()
    {
        aI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerAI>();
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
        if (other.gameObject.CompareTag("Riru"))
        {
            RiruHitBox = other.gameObject;
            other.GetComponent<RiruAI>().Toxin();
        }
    }
    IEnumerator ResetPoisonousFlameRange()
    {
        yield return new WaitForSeconds(0.2f);
        if (RiruHitBox == null)
        {
            aI.DeductedPoints(1);
        }
        Destroy(gameObject);
    }
}
