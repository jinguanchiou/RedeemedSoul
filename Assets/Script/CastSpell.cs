using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    private GameObject Skill_01;
    private GameObject Skill_02;

    private bool Cooling = false;
    public Inventory PlayerWorkingSkill;
    private bool isAttacking = false;

    
    private Transform PlayerTransform;
    private Animator PlayerAnim;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.U) && Skill_01 != null && Skill_01.tag == "TriggeredSkill" && !Cooling)
        {
            Cooling = true;
            SkillAnimeTrigger_01();
            StartCoroutine(StartTime_01());
            StartCoroutine(CoolTime_01());
        }
        else if (Input.GetKeyDown(KeyCode.I) && Skill_02 != null && Skill_02.tag == "TriggeredSkill" && !Cooling)
        {
            Cooling = true;
            SkillAnimeTrigger_02();
            StartCoroutine(StartTime_02());
            StartCoroutine(CoolTime_02());
        }
        else if (Skill_01 != null && Skill_01.tag == "BoolSkill")
        {
            SkillAnimeBool();
            StartCoroutine(StartTime_01());
        }
        else if (Skill_02 != null && Skill_02.tag == "BoolSkill")
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
        if (Skill_01.name == "PoisonousFlame")
            PlayerAnim.SetBool("PoisonousFlame", Input.GetKey(KeyCode.U));
        if (Skill_02.name == "PoisonousFlame")
            PlayerAnim.SetBool("PoisonousFlame", Input.GetKey(KeyCode.I));
    }

    IEnumerator StartTime_01()
    {
        if (Skill_01.tag == "TriggeredSkill")
        {
            yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[0].StartTime);
            Instantiate(Skill_01, transform.position, transform.rotation);
        }
        if (Skill_01.tag == "BoolSkill" && Input.GetKey(KeyCode.U))
        {
            yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[0].StartTime);
            if (!isAttacking)
            {
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

        if (Skill_02.tag == "BoolSkill" && Input.GetKey(KeyCode.I))
        {
            yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[1].StartTime);
            if (!isAttacking)
            {
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
        yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[0].CoolTime);
        Cooling = false;
    }
    IEnumerator CoolTime_02()
    {
        yield return new WaitForSeconds(PlayerWorkingSkill.WorkingSkill[1].CoolTime);
        Cooling = false;
    }
}
