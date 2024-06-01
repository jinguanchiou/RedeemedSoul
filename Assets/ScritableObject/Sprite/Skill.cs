using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public GameObject SkillImage;
    public GameObject SkillOntology;
    public int ManaReduced;
    public float StartTime;
    public float CoolTime;
    public bool CanUse;
    [TextArea]
    public string skillInfo;
}

