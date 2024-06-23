using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public int regainPoint;
    public float startTime;
    public float time;
    public CastSpell Mana;
    public GameObject IceSpike;

    public bool isAttacking_1 = false;
    private bool AttackisFinish = false;
    public bool isAttacking_2 = false;
    public bool isAttacking = false;
    private Animator anim;
    private PolygonCollider2D collider2D;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
        AttackBool();
    }
    void Attack()
    {
        if (Input.GetButtonDown("Attack") && !AttackisFinish && !isAttacking_1)
        {
            isAttacking_1 = true;
            anim.SetTrigger("Attack");
            GameManager.instance.audioManager.Play(1, "seSlash", false);
            if (anim.GetBool("IceEnchant"))
                Instantiate(IceSpike, transform.position, transform.rotation);
            StartCoroutine(StartAttack());
        }
        if (Input.GetButtonDown("Attack") && AttackisFinish && !isAttacking_2)
        {
            isAttacking_2 = true;
            anim.SetTrigger("Attack_2");
            GameManager.instance.audioManager.Play(1, "seSlash", false);
            StartCoroutine(StartAttack_2());
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
        isAttacking_1 = false;
        AttackisFinish = true;
        StartCoroutine(AttackController());
    }
    public void AttackBool()
    {
        if (isAttacking_1 || isAttacking_2)
        {
            isAttacking = true;
        }
        if (!isAttacking_1 && !isAttacking_2)
        {
            isAttacking = false;
        }
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
    IEnumerator AttackController()
    {
        yield return new WaitForSeconds(0.2f);
        AttackisFinish = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D" && other.GetComponent<EnemyMonsterGhost>())
        {
            GameManager.instance.audioManager.Play(1, "seSlashHit", false);
            if (anim.GetBool("Enchant"))
            {
                other.GetComponent<EnemyMonsterGhost>().TakeDamage(damage * 3);
                other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("FireEnchant"))
            {
                other.GetComponent<EnemyMonsterGhost>().TakeDamage(damage);
                other.GetComponent<EnemyMonsterGhost>().Burning(2, 3);
                other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("IceEnchant"))
            {
                other.GetComponent<EnemyMonsterGhost>().TakeDamage(damage);
                other.GetComponent<EnemyMonsterGhost>().Frozen(0.5f, 1);
                other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("PoisonEnchant"))
            {
                other.GetComponent<EnemyMonsterGhost>().TakeDamage(damage);
                other.GetComponent<EnemyMonsterGhost>().Toxin();
                other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else
            {
                other.GetComponent<EnemyMonsterGhost>().TakeDamage(damage);
                other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
        }
        if (other.gameObject.CompareTag("Enemy") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D" && other.GetComponent<EnemyMontherPig>())
        {
            GameManager.instance.audioManager.Play(1, "seSlashHit", false);
            if (anim.GetBool("Enchant"))
            {
                other.GetComponent<EnemyMontherPig>().TakeDamage(damage * 3);
                if (other.GetComponent<EnemyFireMan>())
                    other.GetComponent<EnemyFireMan>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("FireEnchant"))
            {
                other.GetComponent<EnemyMontherPig>().TakeDamage(damage);
                other.GetComponent<EnemyMontherPig>().Burning(2, 3);
                if (other.GetComponent<EnemyFireMan>())
                    other.GetComponent<EnemyFireMan>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("IceEnchant"))
            {
                other.GetComponent<EnemyMontherPig>().TakeDamage(damage);
                other.GetComponent<EnemyMontherPig>().Frozen(0.5f, 1);
                if (other.GetComponent<EnemyFireMan>())
                    other.GetComponent<EnemyFireMan>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("PoisonEnchant"))
            {
                other.GetComponent<EnemyMontherPig>().TakeDamage(damage);
                other.GetComponent<EnemyMontherPig>().Toxin();
                if (other.GetComponent<EnemyFireMan>())
                    other.GetComponent<EnemyFireMan>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else
            {
                other.GetComponent<EnemyMontherPig>().TakeDamage(damage);
                if (other.GetComponent<EnemyFireMan>())
                    other.GetComponent<EnemyFireMan>().PlayerHitMe();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
        }
        if (other.gameObject.CompareTag("Riru") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D" && other.GetComponent<RiruAI>())
        {
            GameManager.instance.audioManager.Play(1, "seSlashHit", false);
            if (anim.GetBool("Enchant"))
            {
                other.GetComponent<RiruAI>().TakeDamage(damage * 3);
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("FireEnchant"))
            {
                other.GetComponent<RiruAI>().TakeDamage(damage);
                other.GetComponent<RiruAI>().Burning(2, 3);
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("IceEnchant"))
            {
                other.GetComponent<RiruAI>().TakeDamage(damage);
                other.GetComponent<RiruAI>().Frozen(0.5f, 1);
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else if (anim.GetBool("PoisonEnchant"))
            {
                other.GetComponent<RiruAI>().TakeDamage(damage);
                other.GetComponent<RiruAI>().Toxin();
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
            else
            {
                other.GetComponent<RiruAI>().TakeDamage(damage);
                Mana.RegainMana(regainPoint);
                playerController.HitEnemy();
            }
        }
    }
}
