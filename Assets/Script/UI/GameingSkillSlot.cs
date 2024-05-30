using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameingSkillSlot : MonoBehaviour
{
    public int GameingSkillSlotID;
    public GameObject CoolTime;
    public void SetAsChild(Skill skill)
    {
        if (skill == null)
        {
            return;
        }
        if (transform.childCount == 0)
        {
            CoolTime.GetComponent<CoolDown>().CoolDownTime = skill.CoolTime;
            GameObject instantiatedSkillPrefab = Instantiate(skill.SkillImage, transform.position, Quaternion.identity);
            GameObject CoolTimePrefab = Instantiate(CoolTime, transform.position, Quaternion.identity);
            CoolTimePrefab.transform.SetParent(instantiatedSkillPrefab.transform);
            instantiatedSkillPrefab.transform.SetParent(transform);
        }
        else if (transform.childCount >= 1)
        {
            return;
        }
    }
}
