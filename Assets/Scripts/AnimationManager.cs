using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject panelLoad;

    public IEnumerator LoadNewLevel(float endValue)
    {
        panelLoad.GetComponent<SpriteRenderer>().material.DOFade(endValue, 2f);
        yield return new WaitForSeconds(2f);
    }
}
