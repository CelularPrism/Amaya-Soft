using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCardSelector : MonoBehaviour
{
    private CardScriptableObject[] cardsArray;
    private List<CardScriptableObject> bannedCards = new List<CardScriptableObject>();
    private List<CardScriptableObject> selectedCards;

    private CardScriptableObject correctCard;

    private bool CheckCard(CardScriptableObject card, CardScriptableObject[] listCard)
    {
        for (int numCard = 0; numCard < listCard.Length; numCard++)
        {
            if (card == listCard[numCard])
            {
                return false;
            }
        }
        return true;
    }

    private void SelectCorrectCard()
    {
        correctCard = new CardScriptableObject();
        List<int> correctNumCards = new List<int>();
        int randNumCard = 0;

        for (int numCard = 0; numCard < selectedCards.Count; numCard++)
        {
            if (CheckCard(selectedCards[numCard], bannedCards.ToArray()))
            {
                correctNumCards.Add(numCard);
            }
        }

        if (correctNumCards.Count == 0)
        {
            foreach (var card in cardsArray)
            {
                if (CheckCard(card, bannedCards.ToArray()))
                {
                    randNumCard = Random.Range(0, selectedCards.Count);
                    correctNumCards.Add(randNumCard);
                    selectedCards[randNumCard] = card;
                    break;
                }
            }
        }

        randNumCard = Random.Range(0, correctNumCards.Count);
        correctCard = selectedCards[randNumCard];
        bannedCards.Add(correctCard);
    }

    public List<CardScriptableObject> CollectCards(int countCards, CardScriptableObject[] cardsArray)
    {
        this.cardsArray = cardsArray;
        selectedCards = new List<CardScriptableObject>();
        for (int numCard = 0; numCard < countCards; numCard++)
        {
            int randNumCard = Random.Range(0, cardsArray.Length);

            while (!CheckCard(cardsArray[randNumCard], selectedCards.ToArray()))
            {
                randNumCard = Random.Range(0, cardsArray.Length);
            }
            selectedCards.Add(cardsArray[randNumCard]);
        }

        SelectCorrectCard();
        return selectedCards;
    }

    public CardScriptableObject GetCorrectCard()
    {
        return correctCard;
    }
}
