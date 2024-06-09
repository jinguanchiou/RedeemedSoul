using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Blinks;
    public int ResistDamage;
    private int Potion;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;
    public bool hasShield = false;
    
    private Coroutine burningCoroutine;
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
    // Start is called before the first frame update
    void Start()
    {
        health = HPInventory.HP;
        HealthBar.HealthCurrent = health;
        
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UsePotion();
    }
    public void Shield(int ShieldHealth)
    {
        ResistDamage += ShieldHealth;
    }
    public void DestroyShield()
    {
        ResistDamage = 0;
    }
    public void DamagePlayer(int damage)
    {
        if (ResistDamage > 0)
        {
            hasShield = true;
            ResistDamage -= damage;
        }
        if(ResistDamage <= 0)
        {
            health += ResistDamage;
            HealthBar.HealthCurrent = health;
            if (!hasShield)
            {
                health -= damage;
                sf.FlashScreen();
                if (health < 0)
                {
                    health = 0;
                }
                HPInventory.HP = health;
                HealthBar.HealthCurrent = health;
                BlinkPlayer(Blinks, time);
                polygonCollider2D.enabled = false;
                GameObject gb = Instantiate(floatPoint, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = "生命 -" + damage.ToString();
                StartCoroutine(ShowPlayerHitBox());
            }
            if (health <= 0)
            {
                rb2d.velocity = new Vector2(0, 0);
                //rb2d.gravityScale = 0.0f;
                GameController.isGameAlive = false;
                anim.SetTrigger("Die");
                Invoke("KillPlayer", dieTime);
            }
            hasShield = false;
        }
    }
    public void BurningPlayer(int damage)
    {
        if (ResistDamage > 0)
        {
            hasShield = true;
            ResistDamage -= damage;
        }
        if (ResistDamage <= 0)
        {
            health += ResistDamage;
            HealthBar.HealthCurrent = health;
            if (!hasShield)
            {
                health -= damage;
                sf.FlashScreen();
                if (health < 0)
                {
                    health = 0;
                }
                HPInventory.HP = health;
                HealthBar.HealthCurrent = health;
                BlinkPlayer(Blinks, time);
                polygonCollider2D.enabled = false;
                GameObject gb = Instantiate(BurningHPPoint, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = "燃燒 -" + damage.ToString();
                StartCoroutine(ShowPlayerHitBox());
            }
            if (health <= 0)
            {
                rb2d.velocity = new Vector2(0, 0);
                //rb2d.gravityScale = 0.0f;
                GameController.isGameAlive = false;
                anim.SetTrigger("Die");
                Invoke("KillPlayer", dieTime);
            }
            hasShield = false;
        }
    }
    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        polygonCollider2D.enabled = true;
    }

    void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.O) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && potionInventory.WorkingPotionList[0].Quantity >= 1)
        {
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
    IEnumerator WaitTextMesh(int point)
    {
        yield return new WaitForSeconds(1);
        GameObject gb = Instantiate(RestoreHPPoint, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = "生命 +" + Potion.ToString();
    }
    void KillPlayer()
    {
        Destroy(gameObject);
    }
    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }
    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for(int i = 0 ; i < numBlinks * 2 ; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
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
            BurningPlayer(damage);
            yield return new WaitForSeconds(1);
        }

        burningCoroutine = null;
    }
}