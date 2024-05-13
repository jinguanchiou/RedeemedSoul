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
    public float JumpSpeed;
    public float ThrustSpeed;
    public float JunpAttackCoolDownTime_log;
    public float ThrustAttackCoolDownTime_log;
    public float DoubleAttackCoolDownTime_log;
    public float EnchantCoolDownTime_log;
    public float startJumpAttackTime;
    public float startThrustAttackTime;
    public float startDoubleAttackTime;
    public float JumpAttackTime;
    public float ThrustAttackTime;
    public float DoubleAttackTime;

    private bool AnimeIsPlaying = false;
    private float JunpAttackCoolDownTime = 0f;
    private float ThrustAttackCoolDownTime = 0f;
    private float DoubleAttackCoolDownTime = 0f;
    private float EnchantCoolDownTime = 0f;

    public GameObject floatPoint;

    public PolygonCollider2D JunpAttackBox;
    public PolygonCollider2D ThrustAttackBox;
    public PolygonCollider2D DoubleAttackBox;

    public SpriteRenderer ThrustAttackEffect;
    public RiruThrustAttack ThrustAttackTurn;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth playerHealth;
    private Rigidbody2D RiruRigidbody;
    private PlayerController playerController;
    private Riru3DController riru3DContuoller;
    // Start is called before the first frame update
    void Start()
    {
        RiruRigidbody = GetComponent<Rigidbody2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        riru3DContuoller = GameObject.FindGameObjectWithTag("Riru3D").GetComponent<Riru3DController>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    void Update()
    {
        CountCoolDownTime();
        if (!AnimeIsPlaying && playerHealth != null)
        {
            riru3DContuoller.turn3D(playerTransform, transform);
            ThrustAttackTurn.turnAttack();
        }
    }
    public override void OnEpisodeBegin()
    {
        
    }
    public override void CollectObservations(VectorSensor sensor)
    {

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float JumpAttackmoveY = actions.ContinuousActions[1];
        
        int JumpAttackAction = actions.DiscreteActions[0];
        int ThrustAttackAction = actions.DiscreteActions[1];
        int DoubleAttackAction = actions.DiscreteActions[2];
        int EnchantAction = actions.DiscreteActions[3];
        
        if (moveX != 0)
        {
            transform.localPosition += new Vector3(moveX, 0.0f) * Time.deltaTime * movementSpeed;
            riru3DContuoller.Walk(true);
        }
        else
        {
            riru3DContuoller.Walk(false);
        }

        if (JunpAttackCoolDownTime <= 0)
        {
            if (JumpAttackAction == 1)
            {
                riru3DContuoller.JunpAttack();
                StartCoroutine(StartJumpAttack(JumpAttackmoveY));
                JunpAttackCoolDownTime = JunpAttackCoolDownTime_log;
            }
        }

        if (ThrustAttackCoolDownTime <= 0)
        {
            if (ThrustAttackAction == 1)
            {
                riru3DContuoller.ThrustAttack();
                StartCoroutine(StartThrustAttack());
                ThrustAttackCoolDownTime = ThrustAttackCoolDownTime_log;
            }
        }

        if (DoubleAttackCoolDownTime <= 0)
        {
            if (DoubleAttackAction == 1)
            {
                riru3DContuoller.DoubleAttack();
                StartCoroutine(StartDoubleAttack());
                DoubleAttackCoolDownTime = DoubleAttackCoolDownTime_log;
            }
        }

        if (EnchantCoolDownTime <= 0)
        {
            if (EnchantAction == 1)
            {
                riru3DContuoller.Enchant();

                EnchantCoolDownTime = EnchantCoolDownTime_log;
            }
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        ActionSegment<int> DiscreteActions = actionsOut.DiscreteActions;

        continousActions[0] = Input.GetAxisRaw("Horizontal");
        continousActions[1] = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyUp(KeyCode.J))
        {
            DiscreteActions[0] = 1;
        }
        else
        {
            DiscreteActions[0] = 0;
        }
        continousActions[2] = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyUp(KeyCode.T))
        {
            DiscreteActions[1] = 1;
        }
        else
        {
            DiscreteActions[1] = 0;
        }
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyUp(KeyCode.U))
        {
            DiscreteActions[2] = 1;
        }
        else
        {
            DiscreteActions[2] = 0;
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyUp(KeyCode.E))
        {
            DiscreteActions[3] = 1;
        }
        else
        {
            DiscreteActions[3] = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void FormJumpAttack(bool JumpAttackHitPlayer)
    {
        if (JumpAttackHitPlayer)
            AddReward(20f);
        else if (!JumpAttackHitPlayer)
        {
            AddReward(-5f);
            Debug.Log("!JumpAttackHitPlayer");
        }
    }
    public void FormThrustAttack(bool ThrustAttackHitPlayer)
    {
        if (ThrustAttackHitPlayer)
            AddReward(10f);
        else if (!ThrustAttackHitPlayer)
        {
            AddReward(-3f);
            Debug.Log("!ThrustAttackHitPlayer");
        }
    }
    public void FormDoubleAttack(bool DoubleAttackHitPlayer)
    {
        if (DoubleAttackHitPlayer)
            AddReward(5f);
        else if (!DoubleAttackHitPlayer)
        {
            AddReward(-2f);
            Debug.Log("!DoubleAttackHitPlayer");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    public void TakeDamage(int PlayerDamage)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = PlayerDamage.ToString();
        health -= PlayerDamage;
        FlashColor(0.1f);
    }
    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    IEnumerator StartJumpAttack(float JumpAttackmoveY)
    {
        yield return new WaitForSeconds(startJumpAttackTime);
        Vector2 JumpVel = new Vector2(JumpAttackmoveY * JumpSpeed * 2f, JumpAttackmoveY * JumpSpeed);
        if (playerTransform.position.x > transform.position.x)
        { RiruRigidbody.velocity = Vector2.up * JumpVel.y + Vector2.right * JumpVel.x; }
        else if (playerTransform.position.x < transform.position.x)
        { RiruRigidbody.velocity = Vector2.up * JumpVel.y + Vector2.left * JumpVel.x; }
        yield return new WaitForSeconds(0.5f);
        JunpAttackBox.enabled = true;
        AnimeIsPlaying = true;
        StartCoroutine(disableJumpAttackHitBox());
    }
    IEnumerator disableJumpAttackHitBox()
    {
        yield return new WaitForSeconds(JumpAttackTime);
        JunpAttackBox.enabled = false;
        yield return new WaitForSeconds(1.5f);
        AnimeIsPlaying = false;
    }

    IEnumerator StartThrustAttack()
    {
        yield return new WaitForSeconds(startThrustAttackTime);
        ThrustAttackBox.enabled = true;
        ThrustAttackEffect.enabled = true;
        if (playerTransform.position.x > transform.position.x)
        { transform.Translate(Vector3.right * ThrustSpeed); }
        else if (playerTransform.position.x < transform.position.x)
        { transform.Translate(Vector3.left * ThrustSpeed); }
        AnimeIsPlaying = true;
        StartCoroutine(disableThrustAttackHitBox());
    }
    IEnumerator disableThrustAttackHitBox()
    {
        yield return new WaitForSeconds(ThrustAttackTime);
        ThrustAttackBox.enabled = false;
        ThrustAttackEffect.enabled = false;
        yield return new WaitForSeconds(1.5f);
        AnimeIsPlaying = false;
    }

    IEnumerator StartDoubleAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(startDoubleAttackTime);
            DoubleAttackBox.enabled = true;
            yield return new WaitForSeconds(startDoubleAttackTime);
            DoubleAttackBox.enabled = false;
        }
    }

    void CountCoolDownTime()
    {
        if(JunpAttackCoolDownTime > 0)
        {
            JunpAttackCoolDownTime -= Time.deltaTime;
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
    }
}