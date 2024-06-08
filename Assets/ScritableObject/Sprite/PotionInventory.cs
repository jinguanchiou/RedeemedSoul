using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/PotionInventory")]
public class PotionInventory : ScriptableObject
{
    public List<PotionType> PotionList = new List<PotionType>();
    public List<PotionType> WorkingPotionList = new List<PotionType>();
}
