using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public ExplodsionRang explodsionRang;
    private PlayerHealth playerHealth;
    public Vector2 startSpeed;
    public float delayExplodeTime;
    public float hitBoxTime;
    public float DestroyBallTime;



    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rb2d.velocity = transform.right * startSpeed.x + transform.up * startSpeed.y;
        if (rb2d.velocity.x > 0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (rb2d.velocity.x < -0.1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
    }
    void CheckGrounded()
    {
        if(rb2d.IsTouchingLayers(LayerMask.GetMask("Ground"))
           || rb2d.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
           || rb2d.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform")) == true)
        {
            Explode();
        }
    }
    void Explode()
    {
        rb2d.velocity = transform.right * 0 + transform.up * 0;
        anim.SetTrigger("Explode");
        Invoke("GenExplodsionRange", hitBoxTime);
        Invoke("DestroyThisBall", DestroyBallTime);
    }
    void GenExplodsionRange()
    {
        Instantiate(explodsionRang, transform.position, Quaternion.identity);
    }
    void DestroyThisBall()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }
}
