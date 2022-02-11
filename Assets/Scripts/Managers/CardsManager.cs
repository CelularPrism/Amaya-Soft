using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    private GameController gameController;
    private Card correctCard;

    private void Start()
    {
        gameController = transform.GetComponent<GameController>();
    }

    public bool TouchCard(Card card)
    {
        if (correctCard == card)
        {
            correctCard = null;
            gameController.NextLevel();
            return true;
        }

        return false;
    }

    public void SetCorrectCard(Card card)
    {
        correctCard = card;
    }
}
