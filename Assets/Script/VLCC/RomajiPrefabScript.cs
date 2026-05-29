using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RomajiPrefabScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string romaji;
    [SerializeField] private Button buttonRomaji;

    public void SetUp(string x)
    {
        romaji = x;
        text.text = x.ToString();
        text.alpha = 0f;
        transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(text.DOFade(1f, 0.6f));
        seq.Join(transform.DOScale(1f, 1f).SetEase(Ease.OutBack));
        seq.SetUpdate(true);

        buttonRomaji.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (VLCCManager.instance.CheckRomajiOrder(romaji))
        {
            VLCCManager.instance.CheckVLCC();
            Destroy(gameObject);
            
        }
    }
}
