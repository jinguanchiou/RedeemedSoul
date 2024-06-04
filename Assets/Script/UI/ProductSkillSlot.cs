using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSkillSlot : MonoBehaviour
{
    public void SetAsChild(Skill skill)
    {
        if (skill == null)
        {
            return;
        }
        if (transform.childCount == 0)
        {
            GameObject instantiatedSkillPrefab = Instantiate(skill.SkillImage, transform.position, Quaternion.identity);
            instantiatedSkillPrefab.transform.SetParent(transform);
        }
        else if (transform.childCount >= 1)
        {
            Transform childTransform_1 = transform.GetChild(0);
            Destroy(childTransform_1);
            GameObject instantiatedSkillPrefab = Instantiate(skill.SkillImage, transform.position, Quaternion.identity);
            instantiatedSkillPrefab.transform.SetParent(transform);
        }
    }
}
