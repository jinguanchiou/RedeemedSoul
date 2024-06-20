using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAI : MonoBehaviour
{
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
    private PlayerControllerAI PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        health = HPInventory.HP;
        HealthBar.HealthCurrent = health;
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerAI>();
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DamagePlayer(int damage)
    {
        if (!hasShield)
        {
            health -= damage;
            sf.FlashScreen();
            if (health <= 0)
            {
                health = 0;
                PlayerController.DeductedPoints(10);
            }
            PlayerController.DeductedPoints(5);
            HPInventory.HP = health;
            HealthBar.HealthCurrent = health;
            BlinkPlayer(Blinks, time);
            polygonCollider2D.enabled = false;
            float randomLocation = Random.Range(minLocation, maxLocation);
            GameObject gb = Instantiate(floatPoint, new Vector3(transform.position.x + randomLocation, transform.position.y + 2 + randomLocation), Quaternion.identity) as GameObject;
            gb.transform.GetChild(0).GetComponent<TextMesh>().text = "¥Í©R -" + damage.ToString();
            StartCoroutine(ShowPlayerHitBox());
        }
    }
    public void BurningPlayer(int damage)
    {
        health -= damage;
        sf.FlashScreen();
        if (health <= 0)
        {
            health = 0;
            PlayerController.DeductedPoints(50);
        }
        PlayerController.DeductedPoints(damage);
        HPInventory.HP = health;
        HealthBar.HealthCurrent = health;
        BlinkPlayer(Blinks, time);
        polygonCollider2D.enabled = false;
        float randomLocation = Random.Range(minLocation, maxLocation);
        GameObject gb = Instantiate(BurningHPPoint, new Vector3(transform.position.x + randomLocation, transform.position.y + 2 + randomLocation), Quaternion.identity) as GameObject;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = "¿U¿N -" + damage.ToString();
        StartCoroutine(ShowPlayerHitBox());
    }
    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCdTime);
        polygonCollider2D.enabled = true;
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
        for (int i = 0; i < numBlinks * 2; i++)
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
