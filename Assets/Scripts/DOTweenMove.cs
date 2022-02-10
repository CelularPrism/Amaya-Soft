using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenMove : MonoBehaviour
{
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 normScale;
    [SerializeField] private float duration = 2f;

    private void Start()
    {
        Appearance();
    }

    public void Appearance()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(2f);
        sequence.Append(transform.DOScale(maxScale, duration));
        sequence.Append(transform.DOScale(minScale, duration));
        sequence.Append(transform.DOScale(normScale, duration));
    }

    public void Disappearance()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(maxScale, duration));
        sequence.Append(transform.DOScale(Vector3.zero, duration));
    }

}
