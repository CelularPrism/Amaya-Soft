using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CollectionCardSelector cardSelector;
    [SerializeField] private GameObject prefab;

    private GameController gameController;
    private Card correctCard;

    private LevelScriptableObject[] levelsArray;
    private LevelScriptableObject nowLevel;

    private List<CardScriptableObject> listCards;

    private int numberLevel;
    private float posColumnY;

    void Start()
    {
        numberLevel = 0;
        gameController = transform.GetComponent<GameController>();
        levelsArray = Resources.LoadAll<LevelScriptableObject>("Levels");
        GenerateLevel();
    }


    private void BuildLevel()
    {
        for (int line = 0; line < nowLevel.countLines; line++) 
        {
            posColumnY = nowLevel.countLines - (prefab.transform.localScale.y / 2 + prefab.transform.localScale.y * line);
            BuildLine(nowLevel.countColumns, line);
        }
    }

    private void BuildLine(int countColumns, int line)
    {
        for (int column = 0; column < countColumns; column++)
        {
            GameObject gameObjectCard = Instantiate(prefab);
            SpriteRenderer spriteRenderer = gameObjectCard.transform.GetChild(0).GetComponent<SpriteRenderer>();
            
            Sprite sprite = listCards[column + line * nowLevel.countColumns].sprite;

            RotationGameObject(sprite, gameObjectCard.transform.GetChild(0));
            spriteRenderer.sprite = sprite;

            float posColumnX = nowLevel.countColumns - (prefab.transform.localScale.x / 2 + prefab.transform.localScale.x * column);
            Vector3 newPos = new Vector3(posColumnX, posColumnY);

            gameObjectCard.transform.SetParent(transform);
            gameObjectCard.transform.localScale = prefab.transform.localScale;
            gameObjectCard.transform.localPosition = newPos;
        }
    }

    private void RotationGameObject(Sprite sprite, Transform transform)
    {
        if (sprite.rect.width / sprite.rect.height > 1.4f)
        {
            transform.Rotate(new Vector3(0f, 0f, -90f));
        } else
        {
            transform.Rotate(Vector3.zero);
        }
    }

    public void ClearLevel()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void GenerateLevel()
    {
        ClearLevel();

        if (numberLevel < levelsArray.Length)
        {
            foreach (var level in levelsArray)
            {
                if (level.numberLevel == numberLevel + 1)
                {
                    numberLevel += 1;
                    nowLevel = level;
                    break;
                }
            }
        }
        else
        {
            Restart();
            return;
        }

        int countCards = nowLevel.countColumns * nowLevel.countLines;

        listCards = cardSelector.CollectCards(countCards, nowLevel.cardsArray);
        BuildLevel();
        gameController.StartGame(cardSelector.GetCorrectCard(), listCards);
    }

    public void Restart()
    {
        numberLevel = 0;
        GenerateLevel();
    }
}
