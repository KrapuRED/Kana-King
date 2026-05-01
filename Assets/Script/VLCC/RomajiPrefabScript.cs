using UnityEngine;
using TMPro;

public class RomajiPrefabScript : MonoBehaviour
{
   [SerializeField] private TMP_Text text;
    [SerializeField] private string romaji;
    public void SetUp(string x)
    {
        romaji = x;
        text.text = x.ToString();
    }
}
