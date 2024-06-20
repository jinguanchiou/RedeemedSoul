using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class RiruAI : Agent
{
    public int health;
    public Transform playerTransform;
    public float movementSpeed;
    public float movementSpeed_log;
    public float JumpSpeed;
    public float ThrustSpeed;
    public float JumpAttackCoolDownTime_log;
    public float ThrustAttackCoolDownTime_log;
    public float DoubleAttackCoolDownTime_log;
    public float EnchantCoolDownTime_log;
    public float BlockCoolDownTime_log;
    public float startJumpAttackTime;
    public float startThrustAttackTime;
    public float startDoubleAttackTime;
    public float JumpAttackTime;
    public float ThrustAttackTime;
    public float DoubleAttackTime;

    private bool AnimeIsPlaying = false;
    private bool isGround;
    private bool wasGrounded;
    private bool hasLanded;
    private bool CanJump;
    private float JumpAttackCoolDownTime = 0f;
    private float ThrustAttackCoolDownTime = 0f;
    private float DoubleAttackCoolDownTime = 0f;
    private float EnchantCoolDownTime = 0f;
    public float BlockCoolDownTime = 0f;
    private float minLocation = -0.5f;
    private float maxLocation = 0.5f;

    public int maxJumpAttacks = 3;
    public float jumpAttackWindow = 2f;
    private int currentJumpAttacks = 0;
    private float jumpAttackTimer = 0f;

    private bool ThrustAttackIsAction;
    private FirePillarList currentFirePillarList;
    private bool FirePillarListIsDestroy;

    public GameObject floatPoint;
    public GameObject FloorEffects;
    public GameObject FirePillarList;
    public GameObject bloodEffect;
    public GameObject dropCoin;

    public BoxCollider2D RiruFeet;
    public PolygonCollider2D JunpAttackBox;
    public PolygonCollider2D ThrustAttackBox;
    public PolygonCollider2D DoubleAttackBox;
    public PolygonCollider2D Block;
    public Transform JumpAttackTransform;
    public Transform ThrustAttackTransform;
    public Transform DoubleAttackTransform;
    public Transform BlockTransform;
    public GameingUIInventory PlayerHP;

    private int toxin;
    private float toxinTime;
    private Coroutine burningCoroutine;
    private Coroutine frozenCoroutine;


    public SpriteRenderer ThrustAttackEffect;
    public RiruThrustAttack ThrustAttackTurn;
    public RiruJumpAttack JumpAttackTurn;
    public RiruDoubleAttack riruDoubleTurn;
    public RiruSight sight;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealthAI playerHealth;//之後要改回PlayerHealth
    private Rigidbody2D RiruRigidbody;
    private PlayerControllerAI playerController;
    private Transform PlayerTransfotm;
    private Riru3DController riru3DContuoller;
    // Start is called before the first frame update
    void Start()
    {
        RiruRigidbody = GetComponent<Rigidbody2D>();
        RiruFeet = GetComponent<BoxCollider2D>();
        PlayerTransfotm = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthAI>(); //之後要改回PlayerHeath
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerAI>();
        riru3DContuoller = GameObject.FindGameObjectWithTag("Riru3D").GetComponent<Riru3DController>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        movementSpeed_log = movementSpeed;
        CanJump = true;
    }
    void Update()
    {
        CountCoolDownTime();
        CheckGrounded();
        ToxinTime();
        ToPlayerRange();
        if (!AnimeIsPlaying && playerHealth != null)
        {
            riru3DContuoller.turn3D(playerTransform, transform);
            ThrustAttackTurn.turnAttack();
        }
        if(ThrustAttackCoolDownTime >= ThrustAttackCoolDownTime_log - 0.1f)
        {
            ThrustAttackIsAction = true;
        }
    }
    void CheckGrounded()
    {
        isGround = RiruFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGround && !wasGrounded)
        {
            if (!hasLanded)
            {
                OnLanding();
                hasLanded = true;
            }
        }
        else if (!isGround)
        {
            hasLanded = false;
        }
        wasGrounded = isGround;
    }
    void ToPlayerRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= 5.5)
        {
            AddReward(0.01f);
        }
        if (distanceToPlayer > 5.5 && distanceToPlayer < 12)
        {
            AddReward(0.01f);
        }
        if (distanceToPlayer >= 12)
        {
            AddReward(-0.005f);
        }
    }
    void OnLanding()
    {
        Instantiate(FloorEffects, new Vector2(transform.position.x, transform.position.y - 2), transform.rotation);
    }
    public override void OnEpisodeBegin()
    {
        
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((float)(PlayerHP.HP/PlayerHP.HP_MAX));//歸一化
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
        sensor.AddObservation((Vector2)transform.localPosition);
        sensor.AddObservation((Vector2)PlayerTransfotm.localPosition);
        sensor.AddObservation(riru3DContuoller.isWalk);
        sensor.AddObservation(riru3DContuoller.isIdle);
        sensor.AddObservation(riru3DContuoller.isJumpAttack);
        sensor.AddObservation(riru3DContuoller.isThrustAttack);
        sensor.AddObservation(riru3DContuoller.isDoubleAttack);
        sensor.AddObservation(riru3DContuoller.isBlocking);
        sensor.AddObservation((Vector2)JumpAttackTransform.transform.localPosition);
        sensor.AddObservation((Vector2)ThrustAttackTransform.transform.localPosition);
        sensor.AddObservation((Vector2)DoubleAttackTransform.transform.localPosition);
        sensor.AddObservation((Vector2)BlockTransform.transform.localPosition);
        sensor.AddObservation((Vector2)JunpAttackBox.transform.localPosition);
        sensor.AddObservation((Vector2)ThrustAttackBox.transform.localPosition);
        sensor.AddObservation((Vector2)DoubleAttackBox.transform.localPosition);
        sensor.AddObservation((Vector2)Block.transform.localPosition);
        sensor.AddObservation(JumpAttackCoolDownTime <= 0);
        sensor.AddObservation(ThrustAttackCoolDownTime <= 0);
        sensor.AddObservation(DoubleAttackCoolDownTime <= 0);
        sensor.AddObservation(BlockCoolDownTime <= 0);
        sensor.AddObservation(currentJumpAttacks);
        sensor.AddObservation(jumpAttackTimer);
        sensor.AddObservation(RiruRigidbody.velocity);

        Vector2 toPlayer = playerTransform.position - transform.position;
        sensor.AddObservation(toPlayer.normalized);
        sensor.AddObservation(toPlayer.magnitude);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float JumpAttackmoveY = actions.ContinuousActions[1];
        
        int JumpAttackAction = actions.DiscreteActions[0];
        int ThrustAttackAction = actions.DiscreteActions[1];
        int DoubleAttackAction = actions.DiscreteActions[2];
        int EnchantAction = actions.DiscreteActions[3];

        

        if (moveX != 0 && !riru3DContuoller.isDoubleAttack && !riru3DContuoller.isJumpAttack && !riru3DContuoller.isThrustAttack && !riru3DContuoller.isBlocking && !riru3DContuoller.isActioning)
        {
            transform.localPosition += new Vector3(moveX, 0.0f) * Time.deltaTime * movementSpeed;
            riru3DContuoller.Walk(true);
            if (BlockCoolDownTime <= 0)
            {
                Block.enabled = true;
            }
        }
        else
        {
            riru3DContuoller.Walk(false);
            Block.enabled = false;
        }
        if (JumpAttackCoolDownTime <= 0)
        {
            if (JumpAttackAction == 1 && !riru3DContuoller.isWalk && riru3DContuoller.isIdle && CanJump)
            {
                CanJump = false;
                riru3DContuoller.JunpAttack();
                if (JumpAttackmoveY > 0.1f)
                {
                    AddReward(2);
                }
                if (JumpAttackmoveY <= 0)
                {
                    AddReward(-2);
                }
                StartCoroutine(StartJumpAttack(JumpAttackmoveY));
                currentJumpAttacks++;
            }
            if (currentJumpAttacks < maxJumpAttacks)
            {
                jumpAttackTimer = jumpAttackWindow;
            }
            else if(currentJumpAttacks >= maxJumpAttacks)
            {
                Debug.Log(currentJumpAttacks);
                currentJumpAttacks = 0;
                JumpAttackCoolDownTime = JumpAttackCoolDownTime_log;
            }
        }
        if (jumpAttackTimer > 0)
        {
            
            jumpAttackTimer -= Time.deltaTime;
            if (jumpAttackTimer <= 0)
            {
                currentJumpAttacks = 0;
                JumpAttackCoolDownTime = JumpAttackCoolDownTime_log;
            }
        }
       
        if (ThrustAttackCoolDownTime <= 0)
        {
            if (ThrustAttackAction == 1 && !riru3DContuoller.isWalk && riru3DContuoller.isIdle)
            {
                riru3DContuoller.ThrustAttack();
                StartCoroutine(StartThrustAttack());
                ThrustAttackCoolDownTime = ThrustAttackCoolDownTime_log;
            }
        }
        if (DoubleAttackCoolDownTime <= 0)
        {
            if (DoubleAttackAction == 1 && !riru3DContuoller.isWalk && riru3DContuoller.isIdle)
            {
                riru3DContuoller.DoubleAttack();
                StartCoroutine(StartDoubleAttack());
                DoubleAttackCoolDownTime = DoubleAttackCoolDownTime_log;
            }
        }
        if (EnchantCoolDownTime <= 0)
        {
            if (EnchantAction == 1 && !riru3DContuoller.isWalk && riru3DContuoller.isIdle)
            {
                riru3DContuoller.Enchant();

                EnchantCoolDownTime = EnchantCoolDownTime_log;
            }
        }
        AddReward(-0.001f);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        ActionSegment<int> DiscreteActions = actionsOut.DiscreteActions;

        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");

        DiscreteActions[0] = Input.GetKey(KeyCode.J) ? 1 : 0;

        continousActions[2] = Input.GetAxisRaw("Horizontal");
        DiscreteActions[1] = Input.GetKey(KeyCode.T) ? 1 : 0;
        DiscreteActions[2] = Input.GetKey(KeyCode.U) ? 1 : 0;
        DiscreteActions[3] = Input.GetKey(KeyCode.E) ? 1 : 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void FormBlock(bool BlockSkill)
    {
        if (BlockSkill)
            AddReward(1f);
    }
    public void FormJumpAttack(bool JumpAttackHitPlayer)
    {
        if (JumpAttackHitPlayer)
            AddReward(6f);
        else if (!JumpAttackHitPlayer)
        {
            //AddReward(-1f);
            Debug.Log("!JumpAttackHitPlayer");
        }
    }
    public void FormThrustAttack(bool ThrustAttackHitPlayer)
    {
        if (ThrustAttackHitPlayer)
            AddReward(3f);
        else if (!ThrustAttackHitPlayer)
        {
            //AddReward(-3f);
            Debug.Log("!ThrustAttackHitPlayer");
        }
    }
    public void FormFirePillar(bool HitPlayer)
    {
        if (HitPlayer && ThrustAttackIsAction)
        {
            AddReward(3f);
            ThrustAttackIsAction = false;
            FirePillarListIsDestroy = false;
            Debug.Log("FirePillarHitPlayer");
        }
        else if (FirePillarListIsDestroy && ThrustAttackIsAction)
        {
            //AddReward(-3f);
            ThrustAttackIsAction = false;
            FirePillarListIsDestroy = false;
            Debug.Log("!FirePillarHitPlayer");
        }
        else
            FirePillarListIsDestroy = false;
    }
    public void FormDoubleAttack(bool DoubleAttackHitPlayer)
    {
        if (DoubleAttackHitPlayer)
            AddReward(3f);
        else if (!DoubleAttackHitPlayer)
        {
            AddReward(-0.5f);
            Debug.Log("!DoubleAttackHitPlayer");
        }
    }
    public void DecPoint(int Dec)
    {
        AddReward(-Dec);
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    public void TakeDamage(int damage)
    {
        float randomLocation = Random.Range(minLocation, maxLocation);
        GameObject gb = Instantiate(floatPoint, new Vector3(transform.position.x + randomLocation, transform.position.y + randomLocation + 4), Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        health -= damage;
        AddReward(-damage * 0.005f);
        playerController.AddPoints(damage);
        FlashColor(0.25f);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameController.camShake.Shake();
        if(health <= 0)
        {
            AddReward(-10);
        }
    }
    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    public void Burning(int damage, int frequency)
    {
        if (burningCoroutine != null)
        {
            StopCoroutine(burningCoroutine);
        }
        burningCoroutine = StartCoroutine(BurningIE(damage, frequency));
    }
    IEnumerator BurningIE(int damage, int frequency)
    {
        for (int i = 0; i < frequency; i++)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }
        burningCoroutine = null;
    }
    public void Frozen(float DecreasePercentage, float duration)
    {
        if (frozenCoroutine != null)
        {
            StopCoroutine(frozenCoroutine);
        }
        frozenCoroutine = StartCoroutine(FrozenIE(DecreasePercentage, duration));
    }
    IEnumerator FrozenIE(float DecreasePercentage, float duration)
    {
        if (movementSpeed_log == movementSpeed)
        {
            movementSpeed *= DecreasePercentage;
        }
        yield return new WaitForSeconds(duration);
        movementSpeed = movementSpeed_log;
        frozenCoroutine = null;
    }
    public void Toxin()
    {
        if (toxin < 8)
        {
            toxin++;
        }
        toxinTime = 3;
        TakeDamage(toxin);
        playerController.AddPoints(toxin);
    }
    void ToxinTime()
    {
        if (toxinTime > 0)
        {
            toxinTime -= Time.deltaTime;
        }
        if (toxinTime <= 0)
        {
            toxin = 0;
        }
    }

    IEnumerator StartJumpAttack(float JumpAttackmoveY)
    {
        yield return new WaitForSeconds(startJumpAttackTime);

        Vector2 jumpDirection = Vector2.right;
        Vector2 jumpY = Vector2.up;
        RiruRigidbody.velocity = Vector2.zero;

        if (playerTransform.position.x > transform.position.x)
        {
            RiruRigidbody.AddForce((jumpDirection + jumpY) * JumpAttackmoveY * JumpSpeed * 2, ForceMode2D.Impulse);
        }
        else if (playerTransform.position.x < transform.position.x)
        {
            RiruRigidbody.AddForce((-jumpDirection + jumpY) * JumpAttackmoveY * JumpSpeed * 2, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(0.1f);
        JunpAttackBox.enabled = true;
        AnimeIsPlaying = true;
        StartCoroutine(disableJumpAttackHitBox());
    }
    IEnumerator disableJumpAttackHitBox()
    {
        yield return new WaitForSeconds(JumpAttackTime);
        JunpAttackBox.enabled = false;
        yield return new WaitForSeconds(1.5f);
        CanJump = true;
        AnimeIsPlaying = false;
    }

    IEnumerator StartThrustAttack()
    {
        yield return new WaitForSeconds(startThrustAttackTime);
        ThrustAttackBox.enabled = true;
        ThrustAttackEffect.enabled = true;
        
        AnimeIsPlaying = true;
        if (playerTransform.position.x > transform.position.x)
        { Vector2 ThrustDirection = transform.right; RiruRigidbody.AddForce(ThrustDirection * ThrustSpeed, ForceMode2D.Impulse); StartCoroutine(disableThrustAttackHitBox(1)); }
        else if (playerTransform.position.x < transform.position.x)
        { Vector2 ThrustDirection = -transform.right; RiruRigidbody.AddForce(ThrustDirection * ThrustSpeed, ForceMode2D.Impulse); StartCoroutine(disableThrustAttackHitBox(-1)); }
    }
    IEnumerator disableThrustAttackHitBox(int direction)
    {
        yield return new WaitForSeconds(ThrustAttackTime);
        RiruRigidbody.velocity = Vector2.zero;
        ThrustAttackBox.enabled = false;
        ThrustAttackEffect.enabled = false;
        if (direction > 0)
            StartCoroutine(SpawnAndMonitorFirePillarList(-10, transform.rotation));
        else if (direction < 0)
            StartCoroutine(SpawnAndMonitorFirePillarList(10, Quaternion.Euler(0, 180, 0)));
        yield return new WaitForSeconds(1.5f);
        AnimeIsPlaying = false;
    }
    IEnumerator SpawnAndMonitorFirePillarList(float distance, Quaternion rotation)
    {
        GameObject firePillarListObject = Instantiate(FirePillarList, new Vector2(transform.position.x + distance, transform.position.y - 3.53f), rotation);
        currentFirePillarList = firePillarListObject.GetComponent<FirePillarList>();
        while (currentFirePillarList != null)
        {
            yield return null;
        }
        FirePillarListIsDestroy = true;
        FormFirePillar(false);
    }
    

    IEnumerator StartDoubleAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(startDoubleAttackTime + (i * 0.1f));
            DoubleAttackBox.enabled = true;
            yield return new WaitForSeconds(startDoubleAttackTime + (i * 0.1f));
            DoubleAttackBox.enabled = false;
        }
    }

    void CountCoolDownTime()
    {
        if(JumpAttackCoolDownTime > 0)
        {
            JumpAttackCoolDownTime -= Time.deltaTime;
        }
        if(ThrustAttackCoolDownTime > 0)
        {
            ThrustAttackCoolDownTime -= Time.deltaTime;
        }
        if (DoubleAttackCoolDownTime > 0)
        {
            DoubleAttackCoolDownTime -= Time.deltaTime;
        }
        if (EnchantCoolDownTime > 0)
        {
            EnchantCoolDownTime -= Time.deltaTime;
        }
        if (BlockCoolDownTime > 0)
        {
            BlockCoolDownTime -= Time.deltaTime;
        }
    }
}