using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIInventory : MonoBehaviour
{
    static SkillUIInventory instance;
    public Inventory PlayerBag;
    public FusionInventory WorkingSkill;
    public GameObject SkillBar;
    public GameObject emptySkillSlot;
    public GameObject WorkinSkillSlot_1;
    public GameObject WorkinSkillSlot_2;
    public GameObject productSkillSlot;
    //public TextMesh SkillInfromation;

    public List<GameObject> SkillSlot = new List<GameObject>();
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
        for (int i = 0; i < instance.PlayerBag.SkillList.Count; i++)
        {
            instance.SkillSlot.Add(Instantiate(instance.emptySkillSlot));
            instance.SkillSlot[i].transform.SetParent(instance.SkillBar.transform);
            instance.SkillSlot[i].GetComponent<SkillSlot>().SkillSlotID = i;
            if (instance.PlayerBag.SkillList[i] != null)
            {
                if(instance.PlayerBag.SkillList[i].CanUse)
                    instance.SkillSlot[i].GetComponent<SkillSlot>().SetAsChild(instance.PlayerBag.SkillList[i]);
            }
        }
    }
    public void ResetWorkSlot()
    {
        if (WorkinSkillSlot_1.GetComponent<FusionSkillSlot>().transform.childCount != 0)
        {
            Transform childTransform_1 = WorkinSkillSlot_1.GetComponent<FusionSkillSlot>().transform.GetChild(0);
            Destroy(childTransform_1.gameObject);
            WorkingSkill.FusionSkill[0] = null;
        }
        if (WorkinSkillSlot_2.GetComponent<FusionSkillSlot>().transform.childCount != 0)
        {
            Transform childTransform_2 = WorkinSkillSlot_2.GetComponent<FusionSkillSlot>().transform.GetChild(0);
            Destroy(childTransform_2.gameObject);
            WorkingSkill.FusionSkill[1] = null;
        }
        if (productSkillSlot.GetComponent<ProductSkillSlot>().transform.childCount != 0)
        {
            Transform childTransform = productSkillSlot.GetComponent<ProductSkillSlot>().transform.GetChild(0);
            Destroy(childTransform.gameObject);
        }
    }
    public void FusionSkill()
    {
        Skill RightSkill = WorkingSkill.FusionSkill[0];
        Skill LeftSkill = WorkingSkill.FusionSkill[1];
        if(RightSkill.skillName == "FireBall" && LeftSkill.skillName == "FireBall")
        {
            ResetWorkSlot();
            productSkillSlot.GetComponent<ProductSkillSlot>().SetAsChild(instance.PlayerBag.SkillList[5]);
            instance.PlayerBag.SkillList[5].CanUse = true;
        }
    }
}

