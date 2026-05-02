using UnityEngine;
using TMPro;
using DG.Tweening;
public class KatakanaPrefabScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetUp(char x)
    {
        text.text = x.ToString();

        text.alpha = 0f;
        transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(text.DOFade(1f, 0.6f));
        seq.Join(transform.DOScale(1f, 1f).SetEase(Ease.OutBack));

    }
}
