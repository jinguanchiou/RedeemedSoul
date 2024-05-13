using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    public GameObject Skill_01;
    public GameObject Skill_02;
    public GameObject Skill_03;
    public float startTime;
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
        if(Input.GetKeyDown(KeyCode.U) && Skill_01 != null && Skill_01.tag == "TriggeredSkill")
        {
            SkillAnimeTrigger_01();
            StartCoroutine(StartTime_01());
        }
        if (Input.GetKeyDown(KeyCode.I) && Skill_02 != null && Skill_02.tag == "TriggeredSkill")
        {
            SkillAnimeTrigger_02();
            StartCoroutine(StartTime_02());
        }
        if (Input.GetKeyDown(KeyCode.O) && Skill_03 != null && Skill_03.tag == "TriggeredSkill")
        {
            SkillAnimeTrigger_03();
            StartCoroutine(StartTime_03());
        }
        if (Skill_01 != null && Skill_01.tag == "BoolSkill")
        {
            SkillAnimeBool_01();
            StartCoroutine(StartTime_01());
        }
        if (Skill_02 != null && Skill_02.tag == "BoolSkill")
        {
            SkillAnimeBool_02();
            StartCoroutine(StartTime_02());
        }
        if (Skill_03 != null && Skill_03.tag == "BoolSkill")
        {
            SkillAnimeBool_03();
            StartCoroutine(StartTime_03());
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
    }
    public void SkillAnimeTrigger_02()
    {
        if (Skill_02.name == "FireBall")
            PlayerAnim.SetTrigger("FireBall");
        else if (Skill_02.name == "IceSpike")
            PlayerAnim.SetTrigger("IceSpike");
        else if (Skill_02.name == "Shield")
            PlayerAnim.SetTrigger("Shield");
    }
    public void SkillAnimeTrigger_03()
    {
        if (Skill_03.name == "FireBall")
            PlayerAnim.SetTrigger("FireBall");
        else if (Skill_03.name == "IceSpike")
            PlayerAnim.SetTrigger("IceSpike");
        else if (Skill_03.name == "Shield")
            PlayerAnim.SetTrigger("Shield");
    }
    public void SkillAnimeBool_01()
    {
        if (Skill_01.name == "PoisonousFlame")
            PlayerAnim.SetBool("PoisonousFlame", Input.GetKey(KeyCode.U));
    }
    public void SkillAnimeBool_02()
    {
        if (Skill_02.name == "PoisonousFlame")
            PlayerAnim.SetBool("PoisonousFlame", Input.GetKey(KeyCode.I));
    }
    public void SkillAnimeBool_03()
    {
        if (Skill_03.name == "PoisonousFlame")
            PlayerAnim.SetBool("PoisonousFlame", Input.GetKey(KeyCode.O));
    }
    IEnumerator StartTime_01()
    {
        if (Skill_01.name == "FireBall" || Skill_01.name == "IceSpike")
        {
            yield return new WaitForSeconds(startTime);
            Instantiate(Skill_01, transform.position, transform.rotation);
        }
        if(Skill_01.name == "Shield")
        {
            yield return new WaitForSeconds(startTime);
            Instantiate(Skill_01, PlayerTransform.position, PlayerTransform.rotation);
        }
        if (Skill_01.name == "PoisonousFlame" && !isAttacking && Input.GetKey(KeyCode.U))
        {
            isAttacking = true;
            yield return new WaitForSeconds(0.2f);
            Instantiate(Skill_01, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
            isAttacking = false;
        }
    }
    IEnumerator StartTime_02()
    {
        if (Skill_02.name == "FireBall" || Skill_02.name == "IceSpike")
        {
            yield return new WaitForSeconds(startTime);
            Instantiate(Skill_02, transform.position, transform.rotation);
        }
        if (Skill_02.name == "Shield")
        {
            yield return new WaitForSeconds(startTime);
            Instantiate(Skill_02, PlayerTransform.position, PlayerTransform.rotation);
        }
        if (Skill_02.name == "PoisonousFlame" && !isAttacking && Input.GetKey(KeyCode.I))
        {
            isAttacking = true;
            yield return new WaitForSeconds(0.2f);
            Instantiate(Skill_02, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
            isAttacking = false;
        }
    }
    IEnumerator StartTime_03()
    {
        if (Skill_03.name == "FireBall" || Skill_03.name == "IceSpike")
        {
            yield return new WaitForSeconds(startTime);
            Instantiate(Skill_03, transform.position, transform.rotation);
        }
        if (Skill_03.name == "Shield")
        {
            yield return new WaitForSeconds(startTime);
            Instantiate(Skill_03, PlayerTransform.position, PlayerTransform.rotation);
        }
        if (Skill_03.name == "PoisonousFlame" && !isAttacking && Input.GetKey(KeyCode.O))
        {
            isAttacking = true;
            yield return new WaitForSeconds(0.2f);
            Instantiate(Skill_03, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
            isAttacking = false;
        }
    }
}
