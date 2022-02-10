using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;

    private Camera mainCamera;
    private CardsManager cardsManager;

    private void Update()
    {
        Vector3 posInput = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = transform.position;
        Vector3 localScale = transform.localScale;

        if (Input.GetMouseButtonDown(0) && CheckInputField(posInput, position, localScale))
        {
            if (!cardsManager.TouchCard(this))
            {
                ErrorBounce();
            }
        }
    }

    private void ErrorBounce()
    {
        float randZ = Random.Range(-20f, 20f);

        Vector3 newPos = new Vector3(0f, 0f, randZ);
        Vector3 oldPos = Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(newPos, duration).SetEase(Ease.InBounce));
        sequence.Append(transform.DORotate(oldPos, duration).SetEase(Ease.InBounce));
    }

    private bool CheckInputField(Vector3 posInput, Vector3 position, Vector3 scale)
    {
        if ((position.x - scale.x / 2 <= posInput.x && position.x + scale.x / 2 >= posInput.x)
        && (position.y - scale.y / 2 <= posInput.y && position.y + scale.y / 2 >= posInput.y))
        {
            return true;
        }
        return false;
    }

    public void SetCardsManager(CardsManager manager)
    {
        cardsManager = manager;
    }

    public void SetMainCamera(Camera camera)
    {
        mainCamera = camera;
    }

}
