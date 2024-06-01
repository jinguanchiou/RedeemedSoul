using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    private GameObject Skill_01;
    private GameObject Skill_02;

    private bool Skill_01Cooling = true;
    private bool Skill_02Cooling = true;
    private bool Cooling_01IsCooling = false;
    private bool Cooling_02IsCooling = false;
    public int ManaPoint;
    public Inventory PlayerWorkingSkill;
    public GameingUIInventory MPInventory;
    public GameObject SkillBar;
    private bool isAttacking = false;

    
    private Transform PlayerTransform;
    private Animator PlayerAnim;
    // Start is called before the first frame update
    void Start()
    {
        ManaPoint = MPInventory.MP;
        ManaBar.ManaCurrent = ManaPoint;
        PlayerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
        // Update is called once per frame
        void Update()
    {
        if(PlayerWorkingSkill.WorkingSkill[0] != null)
        {
            Skill_01 = PlayerWorkingSkill.WorkingSkill[0].SkillOntology;
        }
        if (PlayerWorkingSkill.WorkingSkill[1] != null)
        {
            Skill_02 = PlayerWorkingSkill.WorkingSkill[1].SkillOntology;
        }
        if (Input.GetKeyDown(KeyCode.U) && Skill_01 != null && Skill_01.tag == "TriggeredSkill" && !Skill_01Cooling && ManaPoint >= PlayerWorkingSkill.WorkingSkill[0].ManaReduced && PlayerWorkingSkill.WorkingSkill[0] != null)
        {
            Skill_01Cooling = true;
            GameObject CoolDown_01 = SkillBar.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            CoolDown_01.GetComponent<CoolDown>().CoolDownTime = PlayerWorkingSkill.WorkingSkill[0].CoolTime;
            UseSkill_01();
            SkillAnimeTrigger_01();
            StartCoroutine(StartTime_01());
        }
        if(Skill_01Cooling && !Cooling_01IsCooling && PlayerWorkingSkill.WorkingSkill[0] != null)
        {
            StartCoroutine(CoolTime_01());
        }
        if (Input.GetKeyDown(KeyCode.I) && Skill_02 != null && Skill_02.tag == "TriggeredSkill" && !Skill_02Cooling && ManaPoint >= PlayerWorkingSkill.WorkingSkill[1].ManaReduced && PlayerWorkingSkill.WorkingSkill[1] != null)
        {
            Skill_02Cooling = true;
            GameObject CoolDown_02 = SkillBar.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            CoolDown_02.GetComponent<CoolDown>().CoolDownTime = PlayerWorkingSkill.WorkingSkill[1].CoolTime;
            UseSkill_02();
            SkillAnimeTrigger_02();
            StartCoroutine(StartTime_02());
        }
        if(Skill_02Cooling && !Cooling_02IsCooling && PlayerWorkingSkill.WorkingSkill[1] != null)
        {
            StartCoroutine(CoolTime_02());
        }
        if (Skill_01 != null && Skill_01.tag == "BoolSkill")
        {
            SkillAnimeBool();
            StartCoroutine(StartTime_01());
        }
        if (Skill_02 != null && Skill_02.tag == "BoolSkill")
        {
            SkillAnimeBool();
            StartCoroutine(StartTime_02());
        }
    }
    public void SkillAnimeTrigger_01()
    {
        if (Skill_01.name == "FireBall")
            PlayerAnim.SetTrigger("FireBall");
        else if (Skill_01.name == "IceSpike")
            PlayerAnim.SetTrigger("IceSpike");
        else if (Skill_01.name == "Shield")
            PlayerAnim.SetTrigger("Shield");
        else if (Skill_01.name == "Dush")
            PlayerAnim.SetTrigger("Dush");
    }
    public void SkillAnimeTrigger_02()
    {
        if (Skill_02.name == "FireBall")
            PlayerAnim.SetTrigger("FireBall");
        else if (Skill_02.name == "IceSpike")
            PlayerAnim.SetTrigger("IceSpike");
        else if (Skill_02.name == "Shield")
            PlayerAnim.SetTrigger("Shield");
        else if (Skill_02.name == "Dush")
            PlayerAnim.SetTrigger("Dush");
    }
    public void SkillAnimeBool()
    {
        if (Skill_01.name == "PoisonousFlame" && PlayerWorkingSkill.WorkingSkill[0] != null)
            PlayerAnim.SetBool("PoisonousFlame", ManaPoint >= PlayerWorkingSkill.WorkingSkill[0].ManaReduced && Input.GetKey(KeyCode.U));
        if (Skill_02.name == "PoisonousFlame" && PlayerWorkingSkill.WorkingSkill[1] != null)
            PlayerAnim.SetBool("PoisonousFlame", ManaPoint >= PlayerWorkingSkill.WorkingSkill[1].ManaReduced && Input.GetKey(KeyCode.I));
    }

    IEnumerator StartTime_01()
    {
        if (Skill_01.tag == "TriggeredSkill")
        {
            yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[0].StartTime);
            Instantiate(Skill_01, transform.position, transform.rotation);
        }
        if (Skill_01.tag == "BoolSkill" && Input.GetKey(KeyCode.U) && ManaPoint >= PlayerWorkingSkill.WorkingSkill[0].ManaReduced)
        {
            yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[0].StartTime);
            if (!isAttacking)
            {
                UseSkill_01();
                isAttacking = true;
                yield return new WaitForSeconds(0.2f);
                Instantiate(Skill_01, transform.position, transform.rotation);
                yield return new WaitForSeconds(0.1f);
                isAttacking = false;
            }
        }
    }
    IEnumerator StartTime_02()
    {
        if (Skill_02.tag == "TriggeredSkill")
        {
            yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[1].StartTime);
            Instantiate(Skill_02, transform.position, transform.rotation);
        }

        if (Skill_02.tag == "BoolSkill" && Input.GetKey(KeyCode.I) && ManaPoint >= PlayerWorkingSkill.WorkingSkill[1].ManaReduced)
        {
            yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[1].StartTime);
            if (!isAttacking)
            {
                UseSkill_02();
                isAttacking = true;
                yield return new WaitForSeconds(0.2f);
                Instantiate(Skill_02, transform.position, transform.rotation);
                yield return new WaitForSeconds(0.1f);
                isAttacking = false;
            }
        }
    }
    IEnumerator CoolTime_01()
    {
        Cooling_01IsCooling = true;
        yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[0].CoolTime);
        Skill_01Cooling = false;
        Cooling_01IsCooling = false;
    }
    IEnumerator CoolTime_02()
    {
        Cooling_02IsCooling = true;
        yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[1].CoolTime);
        Skill_02Cooling = false;
        Cooling_02IsCooling = false;
    }
    public void UseSkill_01()
    {
        ManaPoint -= PlayerWorkingSkill.WorkingSkill[0].ManaReduced;
        MPInventory.MP = ManaPoint;
        ManaBar.ManaCurrent = ManaPoint;
    }
    public void UseSkill_02()
    {
        ManaPoint -= PlayerWorkingSkill.WorkingSkill[1].ManaReduced;
        MPInventory.MP = ManaPoint;
        ManaBar.ManaCurrent = ManaPoint;
    }
}
