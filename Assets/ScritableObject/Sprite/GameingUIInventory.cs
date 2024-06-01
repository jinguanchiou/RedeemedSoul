using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/GameingUIInventory")]
public class GameingUIInventory : ScriptableObject
{
    public int HP;
    public int MP;
    public int HP_Log;
    public int MP_Log;
    public int HP_MAX;
    public int MP_MAX;
}
