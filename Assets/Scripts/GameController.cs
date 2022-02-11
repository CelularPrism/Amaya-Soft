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
    private AnimationManager animationManager;

    private LevelScriptableObject[] levelsArray;
    private LevelScriptableObject nowLevel;

    private int numLevel;

    private void Start()
    {
        levelsArray = Resources.LoadAll<LevelScriptableObject>("Levels");
        numLevel = 0;
        SetComponents();
        NextLevel();
    }

    private void SetComponents()
    {
        animationManager = transform.GetComponent<AnimationManager>();
        cardsManager = transform.GetComponent<CardsManager>();
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
    }

    public void NextLevel()
    {
        if (numLevel == levelsArray.Length)
        {
            numLevel = 0;
            animationManager.OpenRestart();
            return;
        }

        foreach (var level in levelsArray)
        {
            if (level.numberLevel == numLevel + 1)
            {
                numLevel += 1;
                nowLevel = level;
                StartCoroutine(GenerateNextLevel());
                break;
            }
        }
    }

    public void StartGame(CardScriptableObject correctCard, List<CardScriptableObject> listCards)
    {
        if (animationManager == null)
            SetComponents();

        animationManager.LoadNewLevel(0f, 3f, true);

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

    public IEnumerator GenerateNextLevel()
    {
        animationManager.LoadNewLevel(1f, 3f, false);
        yield return new WaitForSeconds(5f);

        SetCorrectAnswer("");
        spawner.ClearLevel();
        yield return new WaitForSeconds(1f);
        spawner.GenerateLevel(nowLevel);
        animationManager.LoadNewLevel(0f, 3f, true);
    }
}
