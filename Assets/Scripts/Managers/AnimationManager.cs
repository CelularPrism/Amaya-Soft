using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject panelLoad;

    [Header("Position scene")]
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 normScale;
    [SerializeField] private float duration = 2f;

    [Header("Panel restart")]
    [SerializeField] private Transform panelRestart;
    [SerializeField] private Vector3 scale;

    private void Appearance(Sequence sequence)
    {
        sequence.Append(transform.DOScale(maxScale, duration));
        sequence.Append(transform.DOScale(minScale, duration));
        sequence.Append(transform.DOScale(normScale, duration));
    }

    public void Disappearance(Sequence sequence)
    {
        sequence.Append(transform.DOScale(maxScale, duration));
        sequence.Append(transform.DOScale(Vector3.zero, duration));
    }

    public void OpenRestart()
    {
        Sequence sequence = DOTween.Sequence();
        Disappearance(sequence);
        sequence.Append(panelRestart.DOScale(scale, duration));
    }

    public void CloseRestart()
    {
        panelRestart.DOScale(Vector3.zero, duration);
    }

    public void LoadNewLevel(float endValue, float duration, bool isAppearance)
    {
        Sequence sequence = DOTween.Sequence();
        if (isAppearance)
        {
            sequence.Append(panelLoad.GetComponent<SpriteRenderer>().material.DOFade(endValue, duration));
            Appearance(sequence);
        }
        else 
        { 
            Disappearance(sequence);
            sequence.Append(panelLoad.GetComponent<SpriteRenderer>().material.DOFade(endValue, duration));
        }
    }
}
