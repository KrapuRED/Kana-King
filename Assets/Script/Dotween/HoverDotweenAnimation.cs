using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class HoverDotweenAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleUp = 1.1f;
    private Vector2 targetScale;
    private Vector2 originalScale;
    [SerializeField] private float animDuration = 0.3f;
    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * scaleUp;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(targetScale, animDuration).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(originalScale, animDuration).SetUpdate(true);
    }
}
