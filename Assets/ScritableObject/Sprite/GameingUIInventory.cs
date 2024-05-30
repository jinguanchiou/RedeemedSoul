using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/GameingUIInventory")]
public class GameingUIInventory : ScriptableObject
{
    public int HP;
    public int MP;
    public List<GameObject> SkillList = new List<GameObject>();
}
