using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doulbJumpSpeed;
    public float climbSpeed;
    public float restoreTime;
    public bool OnlockPlayer;
    public bool UseingBoolSkill;
    public bool canOpenMall;
    public PlayerAttack playerAttack;
    public PlayerHealth playerHealth;
    public GameObject JumpSFX;

    private Animator myAnim;
    private Rigidbody2D myRigidbody;
    private Transform PlayerTransform;
    private bool isGround;
    private BoxCollider2D myFeet;
    private PolygonCollider2D HitBox;
    private bool canDoubleJump;
    private bool isOneWayPlatform;

    private bool canDush = true;
    private bool Dushing = false;
    private bool isTeleporting = false;
    private bool isLadder;
    private bool isClimbing;
    private bool isJumping;
    private bool isOpening;
    private bool AlreadyJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;
    private bool HitEnemyBool= false;
    private float playerGravity;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        HitBox = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
        OnlockPlayer = false;
        GameController.isGameAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive == true && !HitEnemyBool && !OnlockPlayer && !UseingBoolSkill)
        {
            CheckAirStatus();
            if (!playerAttack.isAttacking)
            {
                Run();
                Jump();
                Climb();
            }
            if(playerAttack.isAttacking)
            {
                Stop();
            }
            Flip();
            CheclLadder();
            OneWayPlatformCheck();
            ToggleMall();
            //Attack();
        }
        CheckGrounded();
        SwitchAnimation();
    }
    public void IsConversation()
    {
        OnlockPlayer = true;
        myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        if (isGround)
        {
            myRigidbody.velocity = new Vector2(0, 0);
            myAnim.SetBool("Idle", true);
            myAnim.SetBool("Run", false);
        }
    }
    public void UseBool(bool use)
    {
        UseingBoolSkill = use;
        myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
    }
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
                || myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
                || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }
    void CheclLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (plyerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    public void teleportationSkill(float DushSpeed, float DushTime, float CooldownTime)
    {
        if (!Dushing && !OnlockPlayer)
        {
            Dushing = true;
            StartCoroutine(Dush(DushSpeed, DushTime, CooldownTime));
        }
    }
    IEnumerator Dush(float DushSpeed, float DushTime, float CooldownTime)
    {
        canDush = false;
        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0f;
        HitBox.enabled = false;
        playerHealth.hasShield = true;
        myRigidbody.velocity = new Vector2(transform.right.x * DushSpeed, 0f);
        yield return new WaitForSeconds(0.05f);
        playerHealth.hasShield = false;
        yield return new WaitForSeconds(DushTime - 0.05f);
        myRigidbody.gravityScale = originalGravity;
        HitBox.enabled = true;
        Dushing = false;
        yield return new WaitForSeconds(CooldownTime);
        canDush = true;
    }
    void Run()
    {
        if (!Dushing)
        {
            float moveDir = Input.GetAxis("Horizontal");
            Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVel;
            bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnim.SetBool("Run", plyerHasXAxisSpeed);
        }
    }
    void Stop()
    {
        Vector2 playerVel = new Vector2(0, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 JumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * JumpVel;
            }
            else if(!isGround)
            {
                if (canDoubleJump)
                {
                    AlreadyJumping = false;
                    myAnim.SetBool("DoubleJump", true);
                    Instantiate(JumpSFX, new Vector3(transform.position.x, transform.position.y - 2.5f), transform.rotation);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doulbJumpSpeed);
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
        if(isGround)
            canDoubleJump = true;
    }
    void Climb()
    {
        if(isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("Climbing", true);
                myRigidbody.gravityScale = 0.0f;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);

            }
            else
            {
                if(isJumping || isFalling || isDoubleJumping || isDoubleFalling)
                {
                    myAnim.SetBool("Climbing", false);
                }
                else
                {
                    myAnim.SetBool("Climbing", false);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("Climbing", false);
            myRigidbody.gravityScale = playerGravity;
        }
        
    }
    void Attack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            myAnim.SetTrigger("Attack");
        }
    }
    public void HitEnemy()
    {
        StartCoroutine(Hit());
    }
    IEnumerator Hit()
    {
        myRigidbody.velocity = new Vector2(transform.right.x * -10, 0f);
        HitEnemyBool = true;
        yield return new WaitForSeconds(0.1f);
        myRigidbody.velocity = new Vector2(0f, 0f);
        HitEnemyBool = false;
    }
    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if(myRigidbody.velocity.y < 0.0f)
            myAnim.SetBool("Jump", false);
            myAnim.SetBool("Fall", true);
        }
        else if(isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }
        if(myRigidbody.velocity.y < -10f && !myAnim.GetBool("Jump"))
        {
            myAnim.SetBool("Fall", true);
            myAnim.SetBool("Idle", false);
            myAnim.SetBool("Run", false);
        }

        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            myAnim.SetBool("DoubleJump", false);
            myAnim.SetBool("DoubleFall", true);
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
        
    }
    void OneWayPlatformCheck()
    {
        if(isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
            StartCoroutine(RestorePlayerLayer());
        }
    }
    IEnumerator RestorePlayerLayer()
    {
        yield return new WaitForSeconds(0.75f);
        if (gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    void ToggleMall()
    {
        if (canOpenMall && Input.GetKeyDown(KeyCode.E) && isOpening)
        {
            GameObject Merchant = GameObject.Find("Merchant");
            Merchant.GetComponent<Merchant>().CloseMall();
            isOpening = false;
        }
        else if (canOpenMall && Input.GetKeyDown(KeyCode.E))
        {
            GameObject Merchant = GameObject.Find("Merchant");
            Merchant.GetComponent<Merchant>().OpenMall();
            isOpening = true;
        }
    }
    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");
    }
}
