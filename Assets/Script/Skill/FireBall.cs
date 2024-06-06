using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector2 startSpeed;
    public int Damage;
    public float DestroyTime;
    public float becomeSmallerTime;

    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

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
        StartCoroutine(CountDestroyTime());
    }

    void DestroyThisBall()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyMonsterGhost>().TakeDamage(Damage);
            other.GetComponent<EnemyMonsterGhost>().PlayerHitMe();
            DestroyThisBall();
        }
    }
    IEnumerator CountDestroyTime()
    {
        yield return new WaitForSeconds(DestroyTime);
        anim.SetBool("becomeSmaller", true);
        yield return new WaitForSeconds(becomeSmallerTime);
        DestroyThisBall();
    }
}
