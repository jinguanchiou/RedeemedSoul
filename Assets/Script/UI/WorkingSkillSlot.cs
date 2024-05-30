using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkingSkillSlot : MonoBehaviour, IDropHandler
{
    public int WorkingSkillSlotID;
    public Inventory PlayerBag;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        WorkingSkillSlot workingSkillSlot = draggableItem.parentAfterDrag.GetComponentInParent<WorkingSkillSlot>();
        SkillSlot skillSlot = draggableItem.parentAfterDrag.GetComponentInParent<SkillSlot>();

        if (transform.childCount == 0 && draggableItem != null)
        {
            if (workingSkillSlot != null)
            {
                if (PlayerBag.WorkingSkill[WorkingSkillSlotID] != PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID])
                {
                    PlayerBag.WorkingSkill[WorkingSkillSlotID] = PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID];
                    PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID] = null;
                }
            }
            else if(skillSlot != null)
            {
                PlayerBag.WorkingSkill[WorkingSkillSlotID] = PlayerBag.SkillList[draggableItem.parentAfterDrag.GetComponent<SkillSlot>().SkillSlotID];
                PlayerBag.SkillList[draggableItem.parentAfterDrag.GetComponent<SkillSlot>().SkillSlotID] = null;
            }
            draggableItem.parentAfterDrag = transform;
        }
        if (transform.childCount == 1 && draggableItem != null)
        {
            if (workingSkillSlot != null)
            {
                var temp = PlayerBag.WorkingSkill[WorkingSkillSlotID];
                PlayerBag.WorkingSkill[WorkingSkillSlotID] = PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID];
                PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID] = temp;
            }
            else if(skillSlot != null)
            {
                var temp = PlayerBag.WorkingSkill[WorkingSkillSlotID];
                PlayerBag.WorkingSkill[WorkingSkillSlotID] = PlayerBag.SkillList[draggableItem.parentAfterDrag.GetComponent<SkillSlot>().SkillSlotID];
                PlayerBag.SkillList[draggableItem.parentAfterDrag.GetComponent<SkillSlot>().SkillSlotID] = temp;
            }

            Transform childTransform = transform.GetChild(0);
            childTransform.SetParent(draggableItem.parentAfterDrag);
            draggableItem.parentAfterDrag = transform;
        }
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
