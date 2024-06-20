using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAI : MonoBehaviour
{
    public int health;
    public float Duration;
    public GameObject ShieldPoint;
    private Animator anim;
    private Transform PlayerTransform;
    private Rigidbody2D rb2d;
    private PlayerHealthAI playerHealth;
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthAI>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Protect();
    }

    // Update is called once per frame
    void Update()
    {
        Duration -= Time.deltaTime;
        DurationCount();
        if (PlayerTransform != null)
        {
            transform.position = PlayerTransform.position;
            transform.rotation = PlayerTransform.rotation;
        }
    }
    void DurationCount()
    {
        Duration -= Time.deltaTime;
        if (Duration <= 3)
        {
            StartCoroutine(DestroyShield());
        }
    }
    IEnumerator DestroyShield()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            sp.enabled = false;
            yield return new WaitForSeconds(0.5f);
            sp.enabled = true;
        }
        anim.SetTrigger("Disappear");
        ProtectDisappear();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        int ResidualDamage = damage - health;
        health -= damage;
        GameObject gb = Instantiate(ShieldPoint, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = "Å@¬Þ -" + damage.ToString();
        if (health <= 0)
        {
            StartCoroutine(ShieldHealthGone(ResidualDamage));
        }
    }
    IEnumerator ShieldHealthGone(int ResidualDamage)
    {
        anim.SetTrigger("Disappear");
        ProtectDisappear();
        playerHealth.DamagePlayer(ResidualDamage);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    void Protect()
    {
        playerHealth.hasShield = true;
    }
    void ProtectDisappear()
    {
        playerHealth.hasShield = false;
    }
}
