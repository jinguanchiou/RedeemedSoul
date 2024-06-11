using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/ScenseContorller")]
public class ScenseContorller : ScriptableObject
{
    public List<bool> ScenseList = new List<bool>();
    public int i;
}
