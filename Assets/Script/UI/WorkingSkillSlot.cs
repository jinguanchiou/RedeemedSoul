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
        GameObject droppedClone;
        droppedClone = Instantiate(dropped, transform.position, Quaternion.identity);
        DraggableItem draggableItem = droppedClone.GetComponent<DraggableItem>();

        WorkingSkillSlot workingSkillSlot = draggableItem.parentAfterDrag.GetComponentInParent<WorkingSkillSlot>();
        SkillSlot skillSlot = draggableItem.parentAfterDrag.GetComponentInParent<SkillSlot>();
        Debug.Log(transform.childCount);
        if (transform.childCount == 1 && draggableItem != null)
        {
            if (workingSkillSlot != null)
            {
                return;
            }
            else if (skillSlot != null)
            {
                Transform childTransform = transform.GetChild(0);
                Destroy(childTransform.gameObject);
                droppedClone.transform.SetParent(transform);
                PlayerBag.WorkingSkill[WorkingSkillSlotID] = PlayerBag.SkillList[draggableItem.parentAfterDrag.GetComponent<SkillSlot>().SkillSlotID];
            }
        }
        else if (transform.childCount == 0 && draggableItem != null)
        {
            droppedClone.transform.SetParent(transform);
            if (workingSkillSlot != null)
            {
                if (PlayerBag.WorkingSkill[WorkingSkillSlotID] != PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID])
                {
                    PlayerBag.WorkingSkill[WorkingSkillSlotID] = PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID];
                    PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID] = null;
                }
                else if(PlayerBag.WorkingSkill[WorkingSkillSlotID] == PlayerBag.WorkingSkill[draggableItem.parentAfterDrag.GetComponent<WorkingSkillSlot>().WorkingSkillSlotID])
                {
                    Destroy(droppedClone);
                }
            }
            else if(skillSlot != null)
            {
                PlayerBag.WorkingSkill[WorkingSkillSlotID] = PlayerBag.SkillList[draggableItem.parentAfterDrag.GetComponent<SkillSlot>().SkillSlotID];
            }
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
