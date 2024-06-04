using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarManger : MonoBehaviour
{
    static SkillBarManger instance;

    public Inventory PlayerBag;
    public GameObject skillSlot;
    public GameObject SkillBar;
    public GameObject CoolTime;
    public List<GameObject> SkillSlot = new List<GameObject>();
    // Start is called before the first frame update
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
        for (int i = 0; i < instance.PlayerBag.WorkingSkill.Count; i++)
        {
            if (instance.SkillBar.transform.childCount == 0)
                break;
            Destroy(instance.SkillBar.transform.GetChild(i).gameObject);
            SkillSlot.Clear();
        }
        for (int i = 0; i < instance.PlayerBag.WorkingSkill.Count; i++)
        {
            instance.SkillSlot.Add(Instantiate(instance.skillSlot));
            instance.SkillSlot[i].transform.SetParent(instance.SkillBar.transform);
            instance.SkillSlot[i].GetComponent<GameingSkillSlot>().GameingSkillSlotID = i;
            instance.SkillSlot[i].GetComponent<GameingSkillSlot>().SetAsChild(instance.PlayerBag.WorkingSkill[i]);
        }
    }
}
