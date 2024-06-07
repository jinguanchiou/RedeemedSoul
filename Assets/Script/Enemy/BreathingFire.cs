using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingFire : MonoBehaviour
{
    public float startTime;
    public float time;
    public float BurningIntervalTime;

    public float CoolTime;
    public int damage;
    public int BurningDamage;
    public int Frequency;

    private bool CanAttack = true;
    private float CoolTime_log;

    public Animator Anim;
    private PlayerHealth playerHealth;
    private PolygonCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        CoolTime_log = CoolTime;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        collider2D = GetComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (CanAttack == false)
        {
            CoolTime -= Time.deltaTime;
            if (CoolTime <= 0)
            {
                CanAttack = true;
            }
        }
    }
    public void Attack()
    {
        if (CanAttack)
        {
            CanAttack = false;
            CoolTime = CoolTime_log;
            Anim.SetTrigger("BreathingFire");
            StartCoroutine(StartAttack());
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        for (int i = 0; i < 3; i++)
        {
            collider2D.enabled = true;
            yield return new WaitForSeconds(time);
            collider2D.enabled = false;
            yield return new WaitForSeconds(time);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            other.GetComponent<PlayerHealth>().DamagePlayer(damage);
            other.GetComponent<PlayerHealth>().Burning(BurningDamage, Frequency);
        }
    }
}
