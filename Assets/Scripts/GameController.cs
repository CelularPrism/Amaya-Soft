using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Text textFieldAnswer;

    private Spawner spawner;
    private CardsManager cardsManager;
    private DOTweenMove DOTweenMove;
    private AnimationManager animationManager;

    private void SetComponents()
    {
        animationManager = transform.GetComponent<AnimationManager>();
        cardsManager = transform.GetComponent<CardsManager>();
        DOTweenMove = transform.GetComponent<DOTweenMove>();
        spawner = transform.GetComponent<Spawner>();
    }

    private void SetCorrectAnswer(string answer)
    {
        int indexStartText = textFieldAnswer.text.IndexOf(" ") + 1;
        textFieldAnswer.text = textFieldAnswer.text.Substring(0, indexStartText);
        textFieldAnswer.text = textFieldAnswer.text + answer;
    }

    private void SetComponentsCard(int numChild)
    {
        Card card = transform.GetChild(numChild).GetComponent<Card>();
        card.SetCardsManager(cardsManager);
        card.SetMainCamera(mainCamera);
        Debug.Log(transform.childCount);
    }

    public void StartGame(CardScriptableObject correctCard, List<CardScriptableObject> listCards)
    {
        if (animationManager == null)
            SetComponents();

        StartCoroutine(animationManager.LoadNewLevel(0f));
        DOTweenMove.Appearance();

        for (int numCard = 0; numCard < listCards.Count; numCard++)
        {
            SetComponentsCard(numCard);
            if (correctCard == listCards[numCard])
            {
                Card card = transform.GetChild(numCard).GetComponent<Card>();
                cardsManager.SetCorrectCard(card);
                SetCorrectAnswer(correctCard.Name);
            }
        }
    }

    public IEnumerator NextLevel()
    {
        DOTweenMove.Disappearance();
        yield return new WaitForSeconds(2f);

        StartCoroutine(animationManager.LoadNewLevel(255f));
        yield return new WaitForSeconds(2f);

        StartCoroutine(animationManager.LoadNewLevel(0f));
        yield return new WaitForSeconds(2f);
        spawner.GenerateLevel();
    }
}
