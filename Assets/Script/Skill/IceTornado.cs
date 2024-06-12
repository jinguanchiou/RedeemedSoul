using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTornado : MonoBehaviour
{
    public Vector2 startSpeed;
    public int Damage;
    public float InLoopTime;
    public float becomeSmallerTime;
    public float DestroyTime;
    public float DecreasePercentage;
    public float duration;
    public float minAngle;
    public float maxAngle;
    public GameObject SkillIce;

    private bool IsStart;
    private Rigidbody2D rb2d;
    private Animator anim;
    private PolygonCollider2D Trigger;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Trigger = GetComponent<PolygonCollider2D>();

        rb2d.velocity = transform.right * startSpeed.x;
        if (rb2d.velocity.x < 0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (rb2d.velocity.x > -0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CountInLoopTime());
        for (int i = 0; i < (int)(InLoopTime + becomeSmallerTime) / 2; i++)
        {
            if (!IsStart)
            {
                IsStart = true;
                StartCoroutine(DemageInterval());
                ShootIceProjectile();
            }
        }
    }

    void DestroyThisSkill()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMonsterGhost>().TakeDamage(Damage);
            other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
            other.GetComponent<EnemyMonsterGhost>().Frozen(DecreasePercentage, duration);
        }
    }
    IEnumerator CountInLoopTime()
    {
        yield return new WaitForSeconds(InLoopTime);
        anim.SetTrigger("InLoop");
        yield return new WaitForSeconds(becomeSmallerTime);
        anim.SetTrigger("InDestroy");
        yield return new WaitForSeconds(DestroyTime);
        DestroyThisSkill();
    }
    IEnumerator DemageInterval()
    {
        Trigger.enabled = true;
        yield return new WaitForSeconds(0.25f);
        Trigger.enabled = false;
        yield return new WaitForSeconds(0.25f);
        IsStart = false;
    }
    void ShootIceProjectile()
    {
        float randomAngle = Random.Range(minAngle, maxAngle);

        GameObject RighticeProjectile = Instantiate(SkillIce, transform.position, Quaternion.identity);
        RighticeProjectile.transform.Rotate(Vector3.forward, randomAngle);
        GameObject LeftProjectile = Instantiate(SkillIce, transform.position, Quaternion.identity);
        LeftProjectile.transform.Rotate(Vector3.forward, randomAngle + 180);
    }
}
