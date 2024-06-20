using System.Collections;
using UnityEngine;

public class CastSpellAI : MonoBehaviour
{
    private bool[] skillCooling = new bool[14];
    private float minLocation = -0.5f;
    private float maxLocation = 0.5f;
    public int ManaPoint;
    public Inventory PlayerSkill;
    public GameingUIInventory MPInventory;
    public GameObject RestoreMPPoint;
    private bool isAttacking = false;

    private Transform PlayerTransform;
    private Animator PlayerAnim;

    void Start()
    {
        ManaPoint = MPInventory.MP;
        ManaBar.ManaCurrent = ManaPoint;
        PlayerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        for (int i = 0; i < skillCooling.Length; i++)
        {
            skillCooling[i] = false;
        }
    }

    void Update()
    {

    }

    public void SkillAnimeTrigger(int skillIndex, int useSkill)
    {
        if (useSkill == 1 && !skillCooling[skillIndex])
        {
            skillCooling[skillIndex] = true; // Set the skill to cooling state immediately
            StartCoroutine(ExecuteSkill(skillIndex));
        }
    }

    private void TriggerSkillAnimation(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                PlayerAnim.SetTrigger("Shield");
                break;
            case 1:
                PlayerAnim.SetTrigger("FireBall");
                break;
            case 2:
                return; // Do nothing for skill index 2
            case 3:
                PlayerAnim.SetTrigger("Dush");
                break;
            case 4:
                PlayerAnim.SetTrigger("IceSpike");
                break;
            case 5:
                PlayerAnim.SetTrigger("Search");
                break;
            case 6:
            case 7:
            case 8:
            case 9:
                PlayerAnim.SetTrigger("Tornado");
                break;
            case 10:
            case 11:
            case 12:
            case 13:
                break;
        }
    }

    IEnumerator ExecuteSkill(int skillIndex)
    {
        TriggerSkillAnimation(skillIndex);
        yield return new WaitForSeconds(PlayerSkill.SkillList[skillIndex].StartTime);

        UseSkill(skillIndex);
        Instantiate(PlayerSkill.SkillList[skillIndex].SkillOntology, transform.position, transform.rotation);

        StartCoroutine(CoolTime(skillIndex));
    }

    public void SkillAnimeBool(int useSkill_3)
    {
        if (useSkill_3 == 1)
        {
            PlayerAnim.SetBool("PoisonousFlame", true);
            skillCooling[2] = true;
            StartCoroutine(ExecuteSkill_03(2));
        }
        else
        {
            PlayerAnim.SetBool("PoisonousFlame", false);
        }
    }

    IEnumerator ExecuteSkill_03(int skillIndex)
    {
        yield return new WaitForSeconds(PlayerSkill.SkillList[skillIndex].StartTime);

        if (!isAttacking)
        {
            UseSkill(skillIndex);
            isAttacking = true;
            yield return new WaitForSeconds(0.3f);
            Instantiate(PlayerSkill.SkillList[skillIndex].SkillOntology, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.2f);
            isAttacking = false;
        }

        StartCoroutine(CoolTime(skillIndex));
    }

    IEnumerator CoolTime(int skillIndex)
    {
        yield return new WaitForSeconds(PlayerSkill.SkillList[skillIndex].CoolTime);
        skillCooling[skillIndex] = false;
    }

    public void UseSkill(int skillIndex)
    {
        ManaPoint -= PlayerSkill.SkillList[skillIndex].ManaReduced;
        MPInventory.MP = ManaPoint;
        ManaBar.ManaCurrent = ManaPoint;
    }

    public void RegainMana(int Mana)
    {
        if (ManaPoint + Mana < MPInventory.MP_MAX)
        {
            ManaPoint += Mana;
            MPInventory.MP = ManaPoint;
            ManaBar.ManaCurrent = ManaPoint;
            WaitTextMesh(Mana);
        }
        else if (ManaPoint + Mana >= MPInventory.MP_MAX && ManaPoint < MPInventory.MP_MAX)
        {
            Mana = MPInventory.MP_MAX - ManaPoint;
            ManaPoint = MPInventory.MP_MAX;
            MPInventory.MP = ManaPoint;
            ManaBar.ManaCurrent = ManaPoint;
            WaitTextMesh(Mana);
        }
    }

    void WaitTextMesh(int Mana)
    {
        float randomLocation = Random.Range(minLocation, maxLocation);
        GameObject gb = Instantiate(RestoreMPPoint, new Vector3(PlayerTransform.position.x + randomLocation, PlayerTransform.position.y + 2 + randomLocation), Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = "Å]¤O +" + Mana.ToString();
    }
}