using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/PotionType")]
public class PotionType : ScriptableObject
{
    public GameObject PotionImage;
    public int RecoveryAmount;
    public int Quantity;
}
