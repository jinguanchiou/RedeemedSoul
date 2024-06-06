using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/ConversationInventory")]
public class ConversationInventory : ScriptableObject
{
    public List<ConversationFunction> Conversation = new List<ConversationFunction>();
}
