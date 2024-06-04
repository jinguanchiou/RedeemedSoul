using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvectoryManager : MonoBehaviour
{
    static InvectoryManager instance;

    public Inventory PlayerBag;
    public GameObject SkillBar;
    public GameObject emptySkillSlot;
    public GameObject WorkingSkillBar;
    public GameObject WorkingemptySkillSlot;
    //public TextMesh SkillInfromation;

    public List<GameObject> SkillSlot = new List<GameObject>();
    public List<GameObject> WorkingSkillSlot = new List<GameObject>();
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
    }
}
