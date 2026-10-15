using UnityEngine;
using TMPro;
using DG.Tweening;
public class PopUpDamagePrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private float movePopUp;
    [SerializeField] private float animDuration = 0.5f;
    public void SetUpDamagePopUp(float x)
    {
        Sequence s = DOTween.Sequence();
        damageText.text = x.ToString();
        s.Append(transform.DOMoveY(movePopUp,animDuration))
            .Join(damageText.DOFade(0,animDuration))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            }
            );
    }
}
