using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PlayerControllerAI : Agent
{
    //PlayerContorller
    public float runSpeed;
    public float jumpSpeed;
    public float doulbJumpSpeed;
    public float climbSpeed;
    public float restoreTime;
    public bool OnlockPlayer;
    public bool canOpenMall;
    public PlayerAttackAI playerAttack;
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
    private bool HitEnemyBool = false;

    private float playerGravity;

    //PlayerHealth
    public int health;
    public int Blinks;
    public int ResistDamage;
    private int Potion;
    private float minLocation = -0.5f;
    private float maxLocation = 0.5f;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;
    public bool hasShield = false;

    private Coroutine burningCoroutine;
    public GameObject GameOverUI;
    public GameObject floatPoint;
    public GameObject RestoreHPPoint;
    public GameObject BurningHPPoint;
    public GameObject GUIBag;
    public PotionBarManger potionBarManger;
    public InvectoryManager invectoryManager;
    public GameingUIInventory HPInventory;
    public PotionInventory potionInventory;

    private Renderer myRender;
    private Animator anim;
    private ScreenFlash sf;
    private Rigidbody2D rb2d;
    private PolygonCollider2D polygonCollider2D;
    //AI
    private bool PreventDoubleJump;
    private bool DoubleJumpAI;
    private bool CanUsePotionAI = true;
    private bool Turn;
    public CastSpellAI cast;
    private RiruAI riruAI;
    private Riru3DController Riru3D; 
    private PlayerHealthAI playerHealth;
    public RiruJumpAttack riruJumpAttack;
    public RiruDoubleAttack riruDoubleAttack;
    public RiruThrustAttack riruThrustAttack;
    public PlayerAISight sight;
    public Inventory SkillMana;
    public float rewardMultiplier;
    public float punishmentMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerContorller
        Riru3D = GameObject.FindGameObjectWithTag("Riru3D").GetComponent<Riru3DController>();
        riruAI = GameObject.FindGameObjectWithTag("Riru").GetComponent<RiruAI>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthAI>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        HitBox = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
        OnlockPlayer = false;
        GameController.isGameAlive = true;
        //PlayerHealth
        health = HPInventory.HP;
        HealthBar.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        
    }
    public override void OnEpisodeBegin()
    {
        
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(riruAI.health/500); //歸一化
        sensor.AddObservation(playerHealth.health / 100);
        sensor.AddObservation(transform.localRotation);
        sensor.AddObservation(cast.ManaPoint);
        sensor.AddObservation((Vector2)riruAI.transform.localPosition);
        sensor.AddObservation((Vector2)transform.localPosition);
        sensor.AddObservation((Vector2)playerAttack.transform.localPosition);
        sensor.AddObservation((Vector2)riruJumpAttack.transform.localPosition);
        sensor.AddObservation((Vector2)riruDoubleAttack.transform.localPosition);
        sensor.AddObservation((Vector2)riruThrustAttack.transform.localPosition);

        Vector2 toPlayer = riruAI.transform.position - transform.position;
        sensor.AddObservation(toPlayer.normalized);
        sensor.AddObservation(toPlayer.normalized);
        sensor.AddObservation(toPlayer.magnitude);
        float distanceToRiruAI = Vector2.Distance(transform.position, riruAI.transform.position);
        sensor.AddObservation(distanceToRiruAI);

        if (sight.EnemyAttackTransforms != null)
        {
            sensor.AddObservation(sight.EnemyAttackTransforms.Count);
            foreach (var enemyAttackTransform in sight.EnemyAttackTransforms)
            {
                sensor.AddObservation((Vector2)enemyAttackTransform.localPosition);
            }
        }
        if (sight.SkillTransforms != null)
        {
            sensor.AddObservation(sight.SkillTransforms.Count);
            foreach (var enemyAttackTransform in sight.SkillTransforms)
            {
                if (enemyAttackTransform != null)
                {
                    sensor.AddObservation((Vector2)enemyAttackTransform.localPosition);
                }
            }
        }
        sensor.AddObservation(myRigidbody.velocity);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float usemove = actions.ContinuousActions[0];
        int useJump = actions.DiscreteActions[0];
        int useAttack = actions.DiscreteActions[1];
        int usePotion = actions.DiscreteActions[2];
        int useSkill_1 = actions.DiscreteActions[3];
        int useSkill_2 = actions.DiscreteActions[4];
        int useSkill_3 = actions.DiscreteActions[5];
        int useSkill_4 = actions.DiscreteActions[6];
        int useSkill_5 = actions.DiscreteActions[7];
        int useSkill_6 = actions.DiscreteActions[8];
        int useSkill_7 = actions.DiscreteActions[9];
        int useSkill_8 = actions.DiscreteActions[10];
        int useSkill_9 = actions.DiscreteActions[11];
        int useSkill_10 = actions.DiscreteActions[12];
        int useSkill_11 = actions.DiscreteActions[13];
        int useSkill_12 = actions.DiscreteActions[14];
        int useSkill_13 = actions.DiscreteActions[15];
        int useSkill_14 = actions.DiscreteActions[16];

        //PlayerContorller
        if (!Riru3D.isActioning)
        {
            if (GameController.isGameAlive == true && !HitEnemyBool && !OnlockPlayer)
            {
                CheckAirStatus();
                if (!playerAttack.isAttacking)
                {

                    Run(usemove);
                    Jump(useJump);
                    Climb();
                }
                if (playerAttack.isAttacking)
                {
                    Stop();
                }
                Flip();
                CheclLadder();
                OneWayPlatformCheck();
                ToggleMall();
                playerAttack.Attack(useAttack);
            }
            CheckGrounded();
            SwitchAnimation();
            //PlayerHealth
            UsePotion(usePotion);
            cast.SkillAnimeTrigger(0, useSkill_1);
            cast.SkillAnimeTrigger(1, useSkill_2);
            cast.SkillAnimeBool(useSkill_3);
            cast.SkillAnimeTrigger(3, useSkill_4);
            cast.SkillAnimeTrigger(4, useSkill_5);
            cast.SkillAnimeTrigger(5, useSkill_6);
            cast.SkillAnimeTrigger(6, useSkill_7);
            cast.SkillAnimeTrigger(7, useSkill_8);
            cast.SkillAnimeTrigger(8, useSkill_9);
            cast.SkillAnimeTrigger(9, useSkill_10);
            cast.SkillAnimeTrigger(10, useSkill_11);
            cast.SkillAnimeTrigger(11, useSkill_12);
            cast.SkillAnimeTrigger(12, useSkill_13);
            cast.SkillAnimeTrigger(13, useSkill_14);
            AddReward(-0.001f);
        }
    }

    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        ActionSegment<int> DiscreteActions = actionsOut.DiscreteActions;

        continousActions[0] = Input.GetAxisRaw("Horizontal");
        DiscreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
        DiscreteActions[1] = Input.GetKey(KeyCode.J) ? 1 : 0;
        DiscreteActions[2] = Input.GetKey(KeyCode.O) ? 1 : 0;
        DiscreteActions[3] = Input.GetKey(KeyCode.Z) ? 1 : 0;
        DiscreteActions[4] = Input.GetKey(KeyCode.X) ? 1 : 0;
        DiscreteActions[5] = Input.GetKey(KeyCode.C) ? 1 : 0;
        DiscreteActions[6] = Input.GetKey(KeyCode.V) ? 1 : 0;
        DiscreteActions[7] = Input.GetKey(KeyCode.B) ? 1 : 0;
        DiscreteActions[8] = Input.GetKey(KeyCode.N) ? 1 : 0;
        DiscreteActions[9] = Input.GetKey(KeyCode.M) ? 1 : 0;
        DiscreteActions[10] = Input.GetKey(KeyCode.F) ? 1 : 0;
        DiscreteActions[11] = Input.GetKey(KeyCode.G) ? 1 : 0;
        DiscreteActions[12] = Input.GetKey(KeyCode.H) ? 1 : 0;
        DiscreteActions[13] = Input.GetKey(KeyCode.R) ? 1 : 0;
        DiscreteActions[14] = Input.GetKey(KeyCode.T) ? 1 : 0;
        DiscreteActions[15] = Input.GetKey(KeyCode.Y) ? 1 : 0; 
        DiscreteActions[16] = Input.GetKey(KeyCode.Q) ? 1 : 0;
    }
    // Update is called once per frame
    void Update()
    {
        health = playerHealth.health;
    }
    public void AddPoints(int GiveEnemyDamege)
    {
        AddReward(GiveEnemyDamege * 0.1f);
        Debug.Log("Add");
    }
    public void DeductedPoints(int damege)
    {
        AddReward(-damege * 0.1f);
        Debug.Log("Dec");
    }
    //PlayerHealth
    void UsePotion(int usePotion)
    {
        if (usePotion == 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && potionInventory.WorkingPotionList[0].Quantity >= 1 && CanUsePotionAI)
        {
            StartCoroutine(WaitUsePotionAnim());
            Potion = potionInventory.WorkingPotionList[0].RecoveryAmount;
            if (health + Potion < HPInventory.HP_MAX)
            {
                anim.SetTrigger("UsePotion");
                health += Potion;
                HPInventory.HP = health;
                HealthBar.HealthCurrent = health;
                StartCoroutine(WaitTextMesh(Potion));
            }
            else if (health + Potion >= HPInventory.HP_MAX)
            {
                anim.SetTrigger("UsePotion");
                Potion = HPInventory.HP_MAX - health;
                health = HPInventory.HP_MAX;
                HPInventory.HP = health;
                HealthBar.HealthCurrent = health;
                StartCoroutine(WaitTextMesh(Potion));
            }
            potionInventory.WorkingPotionList[0].Quantity -= 1;
            potionBarManger.RefreshPotion();
            if (GUIBag.activeInHierarchy)
            {
                invectoryManager.RefreshSkill();
            }
        }
    }
    IEnumerator WaitUsePotionAnim()
    {
        CanUsePotionAI = false;
        yield return new WaitForSeconds(1);
        CanUsePotionAI = true;
    }
    IEnumerator WaitTextMesh(int point)
    {
        yield return new WaitForSeconds(1);
        float randomLocation = Random.Range(minLocation, maxLocation);
        GameObject gb = Instantiate(RestoreHPPoint, new Vector3(transform.position.x + randomLocation, transform.position.y + 2 + randomLocation), Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = "生命 +" + Potion.ToString();
    }
    //PlayerController
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
        Vector2 directionToEnemy = (riruAI.transform.position - transform.position).normalized;
        Vector2 agentForward = transform.right;
        float dotProduct = Vector2.Dot(agentForward, directionToEnemy);
        float reward = 0.0f;
        if (dotProduct > 0.5f)
        {
            reward += dotProduct * rewardMultiplier;
            Debug.Log(reward);
        }
        if (dotProduct < -0.5f)
        {
            reward -= (1.0f + dotProduct) * punishmentMultiplier;
            Debug.Log(reward);
        }
        AddReward(reward);
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
        myRigidbody.velocity = new Vector2(transform.right.x * DushSpeed, 0f);
        yield return new WaitForSeconds(DushTime);
        myRigidbody.gravityScale = originalGravity;
        HitBox.enabled = true;
        Dushing = false;
        yield return new WaitForSeconds(CooldownTime);
        canDush = true;
    }
    void Run(float usemove)
    {
        if (!Dushing)
        {
            float moveDir = usemove;
            Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVel;
            bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnim.SetBool("Run", plyerHasXAxisSpeed);
        }
        if(0.1f < usemove || -0.1f > usemove)
        {
            AddReward(0.005f);
        }
    }
    void Stop()
    {
        Vector2 playerVel = new Vector2(0, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
    }
    void Jump(int useJump)
    {
        if (useJump == 1 && PreventDoubleJump && !canDoubleJump)
        {
            myAnim.SetBool("Jump", true);
            Vector2 JumpVel = new Vector2(0.0f, jumpSpeed);
            myRigidbody.velocity = Vector2.up * JumpVel;
            PreventDoubleJump = false;
        }
        if (useJump == 0 && !PreventDoubleJump)
        {
            DoubleJumpAI = true;
        }
        if (useJump == 1 && DoubleJumpAI && canDoubleJump)
        {
            AlreadyJumping = false;
            DoubleJumpAI = false;
            PreventDoubleJump = true;
            myAnim.SetBool("DoubleJump", true);
            Instantiate(JumpSFX, new Vector3(transform.position.x, transform.position.y - 2.5f), transform.rotation);
            Vector2 doubleJumpVel = new Vector2(0.0f, doulbJumpSpeed);
            myRigidbody.velocity = Vector2.up * doubleJumpVel;
        }
        if (isGround)
        {
            PreventDoubleJump = true;
            canDoubleJump = false;
            DoubleJumpAI = false;
        }
        if(!isGround)
        {
            canDoubleJump = true;
        }
    }
    void Climb()
    {
        if (isLadder)
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
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)
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
        if (Input.GetButtonDown("Attack"))
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
            if (myRigidbody.velocity.y < 0.0f)
                myAnim.SetBool("Jump", false);
            myAnim.SetBool("Fall", true);
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }
        if (myRigidbody.velocity.y < -10f && !myAnim.GetBool("Jump"))
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
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
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
