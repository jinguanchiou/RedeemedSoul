using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/ConversationFunction")]
public class ConversationFunction : ScriptableObject
{
    public List<string> LevelTextMesh = new List<string>();
    public List<GameObject> avatar = new List<GameObject>();
    public bool LevelAlreadyTold = false;
}
