using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelScriptableObject : ScriptableObject
{
    public int numberLevel;
    public int countLines;
    public int countColumns;
    public CardScriptableObject[] cardsArray;
}
