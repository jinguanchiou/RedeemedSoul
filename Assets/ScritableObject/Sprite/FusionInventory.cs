using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/FusionInventory")]
public class FusionInventory : ScriptableObject
{
    public List<Skill> FusionSkill = new List<Skill>();
}
