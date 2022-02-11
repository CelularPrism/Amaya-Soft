using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card list", menuName = "ScriptableObjects/Card list")]
public class CardListScriptableObject : ScriptableObject
{
    public CardScriptableObject[] cardsArray;
}
