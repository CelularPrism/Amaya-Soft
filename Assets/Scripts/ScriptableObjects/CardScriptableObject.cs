using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Card", menuName = "ScriptableObjects/Card")]
public class CardScriptableObject : ScriptableObject
{
    public string Name;
    public Sprite sprite;
}
