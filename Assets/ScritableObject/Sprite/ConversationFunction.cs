using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/ConversationFunction")]
public class ConversationFunction : ScriptableObject
{
    public List<string> LevelTextMesh = new List<string>();
    public List<Image> avatar = new List<Image>();
    public bool LevelAlreadyTold = false;
}
