using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PotionBarManger : MonoBehaviour
{
    static PotionBarManger instance;

    public PotionInventory PotionBag;
    public GameObject potionSlot;
    public GameObject PotionBar;
    public List<GameObject> PotionSlot = new List<GameObject>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        RefreshPotion();
    }
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    public void RefreshPotion()
    {
        for (int i = 0; i < instance.PotionBag.WorkingPotionList.Count; i++)
        {
            if (instance.PotionBar.transform.childCount == 0)
                break;
            Destroy(instance.PotionBar.transform.GetChild(i).gameObject);
            PotionSlot.Clear();
        }
        for (int i = 0; i < instance.PotionBag.WorkingPotionList.Count; i++)
        {
            instance.PotionSlot.Add(Instantiate(instance.potionSlot));
            instance.PotionSlot[i].transform.SetParent(instance.PotionBar.transform);
            instance.PotionSlot[i].GetComponentInChildren<PotionSlot>().PotionSlotID = i;
            instance.PotionSlot[i].GetComponentInChildren<PotionSlot>().SetAsChild(instance.PotionBag.WorkingPotionList[i]);
            instance.PotionSlot[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = instance.PotionBag.WorkingPotionList[i].Quantity.ToString();
        }
    }
}
