using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/TreasureContorller")]
public class TreasureContorller : ScriptableObject
{
    public List<bool> TreasureList = new List<bool>();
}
