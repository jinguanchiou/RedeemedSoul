using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IDropHandler
{
    public int SkillSlotID;
    public Inventory PlayerBag;
    public void OnDrop(PointerEventData eventData)
    {
        
    }
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
            return;
        }
    }
}
