using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Skill> SkillList = new List<Skill>();
    public List<Skill> WorkingSkill = new List<Skill>();
}
