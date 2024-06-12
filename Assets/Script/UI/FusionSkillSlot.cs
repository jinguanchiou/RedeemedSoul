using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FusionSkillSlot : MonoBehaviour, IDropHandler
{
    public int FusionSkillSlotID;
    public SkillUIInventory skillSlotID;
    public Inventory Skill;
    public FusionInventory FusionSkill;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        GameObject droppedClone;
        droppedClone = Instantiate(dropped, transform.position, Quaternion.identity);
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        DraggableItem draggableItemClone = droppedClone.GetComponent<DraggableItem>();

        FusionSkillSlot fusionSkillSlot = draggableItemClone.parentAfterDrag.GetComponentInParent<FusionSkillSlot>();
        SkillSlot skillSlot = draggableItemClone.parentAfterDrag.GetComponentInParent<SkillSlot>();

        if (transform.childCount == 1 && draggableItemClone != null)
        {
            if (fusionSkillSlot != null)
            {
                return;
            }
            else if (skillSlot != null)
            {
                Transform childTransform = transform.GetChild(0);
                Destroy(childTransform.gameObject);
                droppedClone.transform.SetParent(transform);
                FusionSkill.FusionSkill[FusionSkillSlotID] = null;
                FusionSkill.FusionSkill[FusionSkillSlotID] = Skill.SkillList[draggableItem.parentAfterDrag.GetComponentInParent<SkillSlot>().SkillSlotID];
            }
        }
        else if(transform.childCount == 0 && draggableItemClone != null)
        {
            droppedClone.transform.SetParent(transform);
            FusionSkill.FusionSkill[FusionSkillSlotID] = Skill.SkillList[draggableItem.parentAfterDrag.GetComponentInParent<SkillSlot>().SkillSlotID];
        }
    }
    private void OnEnable()
    {
        if (transform.childCount != 0)
        {
            FusionSkill.FusionSkill[FusionSkillSlotID] = null;
            Transform childTransform = transform.GetChild(0);
            Destroy(childTransform.gameObject);
        }
    }
}
