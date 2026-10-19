using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ShakeAnimationDotween : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform targetTransform;

    [Header("Animation Parameters")]
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private float shakeStrength = 20f;
    [SerializeField] private int shakeVibrato = 10;

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Start()
    {
        originalScale = targetTransform.localScale;
        targetScale = originalScale * scaleMultiplier;

        StartAnimation();
    }

    private void StartAnimation()
    {
        Sequence s = DOTween.Sequence();

        s.Append(targetTransform.DOShakeRotation(animDuration, new Vector3(0, 0, shakeStrength), shakeVibrato))
        .SetLoops(-1, LoopType.Yoyo)
        .SetUpdate(true);
        
    }
}
