using UnityEngine;
using TMPro;
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
        buttonRomaji.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        VLCCManager.instance.CheckRomajiOrder(romaji);
    }
}
