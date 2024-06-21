using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorkingPotionSlot : MonoBehaviour, IDropHandler
{
    public int WorkingPotionSlotID;
    public PotionInventory PotionBag;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        GameObject droppedClone;
        droppedClone = Instantiate(dropped, transform.position, Quaternion.identity);
        DraggableItem draggableItem = droppedClone.GetComponent<DraggableItem>();
        if (transform.childCount == 1 && draggableItem != null)
        {
            if (droppedClone.tag != "Potion")
            {
                Destroy(droppedClone);
                return;
            }
            else if (droppedClone.tag == "Potion")
            {
                Transform childTransform = transform.GetChild(0);
                Destroy(childTransform.gameObject);
                droppedClone.transform.SetParent(transform);
                PotionBag.WorkingPotionList[WorkingPotionSlotID] = PotionBag.PotionList[draggableItem.parentAfterDrag.GetComponent<PotionSlot>().PotionSlotID];
            }
        }
        else if (transform.childCount == 0 && draggableItem != null)
        {
            if (droppedClone.tag != "Potion")
            {
                Destroy(droppedClone);
                return;
            }
            else if (droppedClone.tag == "Potion" && draggableItem.parentAfterDrag.GetComponent<PotionSlot>())
            {
                Destroy(droppedClone);
                droppedClone.transform.SetParent(transform);
                PotionBag.WorkingPotionList[WorkingPotionSlotID] = PotionBag.PotionList[draggableItem.parentAfterDrag.GetComponent<PotionSlot>().PotionSlotID];
            }
        }
    }
    public void SetAsChild(PotionType potion)
    {
        if (potion == null)
        {
            return;
        }
        if (transform.childCount == 0)
        {
            GameObject instantiatedPotionPrefab = Instantiate(potion.PotionImage, transform.position, Quaternion.identity);
            instantiatedPotionPrefab.transform.SetParent(transform);
        }
        else if (transform.childCount >= 1)
        {
            return;
        }
    }
}
