using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvectoryManager : MonoBehaviour
{
    static InvectoryManager instance;

    public Inventory PlayerBag;
    public PotionInventory PotionBag;
    public GameObject SkillBar;
    public GameObject PotionBar;
    public GameObject emptySkillSlot;
    public GameObject WorkingSkillBar;
    public GameObject WorkingPotionBar;
    public GameObject WorkingemptySkillSlot;
    public GameObject emptyPotionSlot;
    public GameObject WorkingemptyPotionSlot;
    //public TextMesh SkillInfromation;

    public List<GameObject> SkillSlot = new List<GameObject>();
    public List<GameObject> WorkingSkillSlot = new List<GameObject>();
    public List<GameObject> PotionSlot = new List<GameObject>();
    public List<GameObject> WorkingPotionSlot = new List<GameObject>();
    private void OnEnable()
    {
        RefreshSkill();
    }
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    public void RefreshSkill()
    {
        for (int i = 0; i < instance.PlayerBag.SkillList.Count; i++)
        {
            if (instance.SkillBar.transform.childCount == 0)
                break;
            Destroy(instance.SkillBar.transform.GetChild(i).gameObject);
            SkillSlot.Clear();
        }
        for (int i = 0; i < instance.PlayerBag.WorkingSkill.Count; i++)
        {
            if (instance.WorkingSkillBar.transform.childCount == 0)
                break;
            Destroy(instance.WorkingSkillBar.transform.GetChild(i).gameObject);
            WorkingSkillSlot.Clear();
        }
        for (int i = 0; i < instance.PotionBag.PotionList.Count; i++)
        {
            if (instance.PotionBar.transform.childCount == 0)
                break;
            Destroy(instance.PotionBar.transform.GetChild(i).gameObject);
            PotionSlot.Clear();
        }
        for (int i = 0; i < instance.PotionBag.WorkingPotionList.Count; i++)
        {
            if (instance.WorkingPotionBar.transform.childCount == 0)
                break;
            Destroy(instance.WorkingPotionBar.transform.GetChild(i).gameObject);
            WorkingPotionSlot.Clear();
        }
        for (int i = 0; i < instance.PlayerBag.SkillList.Count; i++)
        {
            instance.SkillSlot.Add(Instantiate(instance.emptySkillSlot));
            instance.SkillSlot[i].transform.SetParent(instance.SkillBar.transform);
            instance.SkillSlot[i].GetComponent<SkillSlot>().SkillSlotID = i;
            if (instance.PlayerBag.SkillList[i] != null)
            {
                if (instance.PlayerBag.SkillList[i].CanUse)
                    instance.SkillSlot[i].GetComponent<SkillSlot>().SetAsChild(instance.PlayerBag.SkillList[i]);
            }
        }
        for (int i = 0; i < instance.PlayerBag.WorkingSkill.Count; i++)
        {
            instance.WorkingSkillSlot.Add(Instantiate(instance.WorkingemptySkillSlot));
            instance.WorkingSkillSlot[i].transform.SetParent(instance.WorkingSkillBar.transform);
            instance.WorkingSkillSlot[i].GetComponent<WorkingSkillSlot>().WorkingSkillSlotID = i;
            instance.WorkingSkillSlot[i].GetComponent<WorkingSkillSlot>().SetAsChild(instance.PlayerBag.WorkingSkill[i]);
        }
        for (int i = 0; i < instance.PotionBag.PotionList.Count; i++)
        {
            instance.PotionSlot.Add(Instantiate(instance.emptyPotionSlot));
            instance.PotionSlot[i].transform.SetParent(instance.PotionBar.transform);
            instance.PotionSlot[i].GetComponentInChildren<PotionSlot>().PotionSlotID = i;
            instance.PotionSlot[i].GetComponentInChildren<PotionSlot>().SetAsChild(instance.PotionBag.PotionList[i]);
            instance.PotionSlot[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = instance.PotionBag.PotionList[i].Quantity.ToString();
        }
        for (int i = 0; i < instance.PotionBag.WorkingPotionList.Count; i++)
        {
            instance.WorkingPotionSlot.Add(Instantiate(instance.WorkingemptyPotionSlot));
            instance.WorkingPotionSlot[i].transform.SetParent(instance.WorkingPotionBar.transform);
            instance.WorkingPotionSlot[i].GetComponentInChildren<WorkingPotionSlot>().WorkingPotionSlotID = i;
            instance.WorkingPotionSlot[i].GetComponentInChildren<WorkingPotionSlot>().SetAsChild(instance.PotionBag.WorkingPotionList[i]);
            instance.WorkingPotionSlot[i].transform.GetChild(0).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = instance.PotionBag.WorkingPotionList[i].Quantity.ToString();
        }
    }
}
