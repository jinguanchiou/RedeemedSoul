using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSlot : MonoBehaviour
{
    public int PotionSlotID;

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
